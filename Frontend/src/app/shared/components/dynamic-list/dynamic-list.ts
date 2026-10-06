import { CommonModule } from '@angular/common';

import {
  ChangeDetectionStrategy,
  Component,
  DestroyRef,
  OnInit,
  computed,
  inject,
  output,
  signal
} from '@angular/core';

import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

import { ActivatedRoute } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';

import { ListColumn } from '../../models/list-column.model';
import { ListConfiguration } from '../../models/list-configuration.model';
import { DynamicListRow } from '../../models/dynamic-list-row.model';
import { Module } from '../../../features/patient/models/module.model';

import { ModuleService } from '../../../features/patient/services/module.service';

import { ListConfigurationService } from '../../../features/patient/services/list-configuration.service';


@Component({
  selector: 'app-dynamic-list',

  standalone: true,

  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatTooltipModule
  ],

  templateUrl: './dynamic-list.html',

  styleUrl: './dynamic-list.scss',

  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DynamicListComponent
  implements OnInit {


  // =========================================================
  // SERVICES
  // =========================================================

  private readonly configurationService =
    inject(ListConfigurationService);

  private readonly moduleService =
    inject(ModuleService);

  private readonly destroyRef =
    inject(DestroyRef);

  private readonly route =
    inject(ActivatedRoute);


  // =========================================================
  // OUTPUTS
  // =========================================================

  public readonly addClicked =
    output<void>();

  public readonly editClicked =
    output<DynamicListRow>();

  public readonly deleteClicked =
    output<DynamicListRow>();


  // =========================================================
  // STATE
  // =========================================================

  public readonly moduleCode =
    signal('');

  protected readonly configuration =
    signal<ListConfiguration | null>(null);

  protected readonly columns =
    signal<ListColumn[]>([]);

  protected readonly rows =
    signal<DynamicListRow[]>([]);

  protected readonly loading =
    signal(false);

  protected readonly searchText =
    signal('');

  protected readonly currentPage =
    signal(1);

  protected readonly pageSize =
    signal(20);

  protected readonly totalRecords =
    signal(0);


  // =========================================================
  // PAGINATION
  // =========================================================

  protected readonly totalPages =
    computed(() => {

      const total =
        this.totalRecords();

      const size =
        this.pageSize();

      if (size <= 0) {
        return 1;
      }

      return Math.max(
        1,
        Math.ceil(total / size)
      );

    });


  protected readonly hasPreviousPage =
    computed(() =>
      this.currentPage() > 1
    );


  protected readonly hasNextPage =
    computed(() =>
      this.currentPage() <
      this.totalPages()
    );


  protected readonly startRecord =
    computed(() => {

      const total =
        this.totalRecords();

      if (total === 0) {
        return 0;
      }

      return (
        (this.currentPage() - 1) *
        this.pageSize()
      ) + 1;

    });


  protected readonly endRecord =
    computed(() => {

      const total =
        this.totalRecords();

      if (total === 0) {
        return 0;
      }

      return Math.min(
        this.currentPage() *
        this.pageSize(),
        total
      );

    });


  // =========================================================
  // INIT
  // =========================================================

  ngOnInit(): void {

    this.route.paramMap
      .pipe(
        takeUntilDestroyed(
          this.destroyRef
        )
      )
      .subscribe(params => {

        const moduleRoute =
          params.get('moduleRoute');

        if (!moduleRoute) {

          console.error(
            'Module route not found.'
          );

          return;
        }

        this.loadModuleByRoute(
          moduleRoute
        );

      });

  }


  // =========================================================
  // LOAD MODULE
  // =========================================================

  private loadModuleByRoute(
    moduleRoute: string
  ): void {

    this.loading.set(true);

    this.moduleService
      .getActiveModules()
      .pipe(
        takeUntilDestroyed(
          this.destroyRef
        )
      )
      .subscribe({

        next: (modules: Module[]) => {

          const module =
            modules.find(item => {

              const route =
                item.route ?? '';

              const cleanRoute =
                route.replace(/^\/+/, '');

              return (
                cleanRoute.toLowerCase() ===
                moduleRoute.toLowerCase()
              );

            });


          if (!module) {

            console.error(
              `Module not found for route: ${moduleRoute}`
            );

            this.clearList();

            return;
          }


          console.log(
            'Resolved Module:',
            module
          );


          this.moduleCode.set(
            module.moduleCode
          );


          this.loadConfiguration();

        },


        error: error => {

          console.error(
            'Failed to load modules:',
            error
          );

          this.clearList();

        }

      });

  }


  // =========================================================
  // LOAD CONFIGURATION
  // =========================================================

  private loadConfiguration(): void {

    const moduleCode =
      this.moduleCode();


    if (!moduleCode) {

      console.error(
        'Module code is empty.'
      );

      this.loading.set(false);

      return;
    }


    this.loading.set(true);


    this.configurationService
      .getConfiguration(moduleCode)
      .pipe(
        takeUntilDestroyed(
          this.destroyRef
        )
      )
      .subscribe({

        next: configuration => {

          console.log(
            'List Configuration:',
            configuration
          );


          this.configuration.set(
            configuration
          );


          this.columns.set(
            configuration.columns ?? []
          );


          const configuredPageSize =
            configuration.pageSize ?? 20;


          this.pageSize.set(
            configuredPageSize
          );


          this.currentPage.set(1);


          this.loadData(
            configuration.apiEndpoint,
            1,
            configuredPageSize
          );

        },


        error: error => {

          console.error(
            'Failed to load list configuration:',
            error
          );

          this.clearList();

        }

      });

  }


  // =========================================================
  // LOAD DATA
  // =========================================================

  private loadData(
    apiEndpoint: string,
    pageNumber: number,
    pageSize: number
  ): void {

    this.loading.set(true);


    this.configurationService
      .getListData(
        apiEndpoint,
        this.searchText(),
        pageNumber,
        pageSize
      )
      .pipe(
        takeUntilDestroyed(
          this.destroyRef
        )
      )
      .subscribe({

        next: response => {

          console.log(
            'Dynamic List Response:',
            response
          );


          this.rows.set(
            response.data ?? []
          );


          this.totalRecords.set(
            response.totalRecords ?? 0
          );


          this.currentPage.set(
            response.pageNumber ??
            pageNumber
          );


          this.pageSize.set(
            response.pageSize ??
            pageSize
          );


          this.loading.set(false);

        },


        error: error => {

          console.error(
            'Failed to load list data:',
            error
          );


          this.rows.set([]);

          this.totalRecords.set(0);

          this.loading.set(false);

        }

      });

  }


  // =========================================================
  // SEARCH
  // =========================================================

  protected onSearchInput(
    event: Event
  ): void {

    const input =
      event.target as HTMLInputElement;

    this.searchText.set(
      input.value
    );

  }


  protected search(): void {

    const config =
      this.configuration();


    if (!config) {
      return;
    }


    this.currentPage.set(1);


    this.loadData(
      config.apiEndpoint,
      1,
      this.pageSize()
    );

  }


  protected resetSearch(): void {

    this.searchText.set('');

    this.currentPage.set(1);


    const config =
      this.configuration();


    if (!config) {
      return;
    }


    this.loadData(
      config.apiEndpoint,
      1,
      this.pageSize()
    );

  }


  protected refresh(): void {

    const config =
      this.configuration();


    if (!config) {
      return;
    }


    this.loadData(
      config.apiEndpoint,
      this.currentPage(),
      this.pageSize()
    );

  }


  // =========================================================
  // PAGINATION
  // =========================================================

  protected nextPage(): void {

    if (!this.hasNextPage()) {
      return;
    }


    const config =
      this.configuration();


    if (!config) {
      return;
    }


    const nextPage =
      this.currentPage() + 1;


    this.loadData(
      config.apiEndpoint,
      nextPage,
      this.pageSize()
    );

  }


  protected previousPage(): void {

    if (!this.hasPreviousPage()) {
      return;
    }


    const config =
      this.configuration();


    if (!config) {
      return;
    }


    const previousPage =
      this.currentPage() - 1;


    this.loadData(
      config.apiEndpoint,
      previousPage,
      this.pageSize()
    );

  }


  protected goToPage(
    page: number
  ): void {

    if (
      page < 1 ||
      page > this.totalPages() ||
      page === this.currentPage()
    ) {
      return;
    }


    const config =
      this.configuration();


    if (!config) {
      return;
    }


    this.loadData(
      config.apiEndpoint,
      page,
      this.pageSize()
    );

  }


  protected getPageNumbers(): number[] {

    const totalPages =
      this.totalPages();

    const currentPage =
      this.currentPage();

    const pages: number[] = [];

    const maxVisiblePages = 5;


    let startPage =
      Math.max(
        1,
        currentPage -
        Math.floor(
          maxVisiblePages / 2
        )
      );


    let endPage =
      Math.min(
        totalPages,
        startPage +
        maxVisiblePages -
        1
      );


    if (
      endPage - startPage <
      maxVisiblePages - 1
    ) {

      startPage =
        Math.max(
          1,
          endPage -
          maxVisiblePages +
          1
        );

    }


    for (
      let page = startPage;
      page <= endPage;
      page++
    ) {

      pages.push(page);

    }


    return pages;

  }


  // =========================================================
  // ACTIONS
  // =========================================================

  protected add(): void {

    this.addClicked.emit();

  }


  protected edit(
    row: DynamicListRow
  ): void {

    this.editClicked.emit(row);

  }


  protected delete(
    row: DynamicListRow
  ): void {

    this.deleteClicked.emit(row);

  }


  // =========================================================
  // CELL HELPERS
  // =========================================================

  protected getCellValue(
    row: DynamicListRow,
    field: string
  ): string {

    const value =
      row[field];


    if (
      value === null ||
      value === undefined ||
      value === ''
    ) {

      return '';

    }


    return String(value);

  }


  protected getNumberValue(
    row: DynamicListRow,
    field: string
  ): number {

    const value =
      row[field];


    if (
      value === null ||
      value === undefined ||
      value === ''
    ) {

      return 0;

    }


    const numberValue =
      Number(value);


    return Number.isNaN(numberValue)
      ? 0
      : numberValue;

  }


  protected getDateValue(
    row: DynamicListRow,
    field: string
  ): Date | null {

    const value =
      row[field];


    if (!value) {
      return null;
    }


    const date =
      new Date(
        String(value)
      );


    if (
      Number.isNaN(
        date.getTime()
      )
    ) {

      return null;

    }


    return date;

  }


  protected getBooleanValue(
    row: DynamicListRow,
    field: string
  ): boolean {

    const value =
      row[field];


    if (
      typeof value === 'boolean'
    ) {

      return value;

    }


    if (
      value === 1 ||
      value === '1' ||
      String(value).toLowerCase() ===
        'true'
    ) {

      return true;

    }


    return false;

  }


  // =========================================================
  // GENDER
  // =========================================================

  protected getGenderClass(
    row: DynamicListRow,
    field: string
  ): string {

    const value =
      this.getCellValue(
        row,
        field
      )
      .trim()
      .toLowerCase();


    switch (value) {

      case 'male':
        return 'gender-male';

      case 'female':
        return 'gender-female';

      case 'other':
        return 'gender-other';

      default:
        return 'gender-default';

    }

  }


  // =========================================================
  // STATUS
  // =========================================================

  protected getStatusClass(
    row: DynamicListRow,
    field: string
  ): string {

    const value =
      this.getCellValue(
        row,
        field
      )
      .trim()
      .toLowerCase();


    switch (value) {

      case 'active':
      case 'approved':
      case 'completed':
      case 'paid':
      case 'success':
      case 'available':
      case 'confirmed':

        return 'status-success';


      case 'pending':
      case 'processing':
      case 'scheduled':
      case 'waiting':

        return 'status-warning';


      case 'inactive':
      case 'cancelled':
      case 'rejected':
      case 'failed':
      case 'expired':

        return 'status-danger';


      default:

        return 'status-neutral';

    }

  }


  // =========================================================
  // CLEAR
  // =========================================================

  private clearList(): void {

    this.moduleCode.set('');

    this.configuration.set(null);

    this.columns.set([]);

    this.rows.set([]);

    this.totalRecords.set(0);

    this.loading.set(false);

  }

}
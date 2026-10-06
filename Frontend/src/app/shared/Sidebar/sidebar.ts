import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal
} from '@angular/core';

import { RouterLink, RouterLinkActive } from '@angular/router';

import { ModuleService } from '../../features/patient/services/module.service';
import { Module } from '../../features/patient/models/module.model';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SidebarComponent implements OnInit {

  private readonly moduleService = inject(ModuleService);

  protected readonly modules = signal<Module[]>([]);
  protected readonly loading = signal(false);

  ngOnInit(): void {
    this.loadModules();
  }

  private loadModules(): void {
    this.loading.set(true);

    this.moduleService.getActiveModules().subscribe({
      next: modules => {
        this.modules.set(
          modules.filter(module => module.route)
        );

        this.loading.set(false);
      },
      error: error => {
        console.error('Failed to load modules:', error);
        this.modules.set([]);
        this.loading.set(false);
      }
    });
  }
}
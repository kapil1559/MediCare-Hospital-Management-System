import {
  ChangeDetectionStrategy,
  Component,
  input,
  output
} from '@angular/core';

@Component({
  selector: 'app-form-footer',
  standalone: true,
  templateUrl: './form-footer.html',
  styleUrl: './form-footer.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormFooterComponent {

  readonly saveText = input('Save');
  readonly cancelText = input('Cancel');
  readonly saving = input(false);
  readonly disabled = input(false);

  readonly saveClicked = output<void>();
  readonly cancelClicked = output<void>();

  protected save(): void {
    if (!this.disabled() && !this.saving()) {
      this.saveClicked.emit();
    }
  }

  protected cancel(): void {
    this.cancelClicked.emit();
  }
}
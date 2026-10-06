import {
  ChangeDetectionStrategy,
  Component,
  input,
  output
} from '@angular/core';

@Component({
  selector: 'app-form-header',
  standalone: true,
  templateUrl: './form-header.html',
  styleUrl: './form-header.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormHeaderComponent {

  readonly title = input('');
  readonly subtitle = input('');
  readonly mode = input<'new' | 'edit'>('new');

  readonly backClicked = output<void>();

  protected back(): void {
    this.backClicked.emit();
  }
}
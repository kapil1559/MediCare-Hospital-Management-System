import {
  ChangeDetectionStrategy,
  Component,
  input
} from '@angular/core';

@Component({
  selector: 'app-form-section',
  standalone: true,
  templateUrl: './form-section.html',
  styleUrl: './form-section.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormSectionComponent {

  readonly title = input('');
  readonly subtitle = input('');
}
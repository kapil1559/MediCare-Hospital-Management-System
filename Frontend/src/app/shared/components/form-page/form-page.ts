import {
  ChangeDetectionStrategy,
  Component
} from '@angular/core';

@Component({
  selector: 'app-form-page',
  standalone: true,
  imports: [],
  templateUrl: './form-page.html',
  styleUrl: './form-page.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormPageComponent {
}
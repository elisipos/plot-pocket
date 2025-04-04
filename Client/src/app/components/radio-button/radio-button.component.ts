import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-radio-button',
  imports: [CommonModule],
  templateUrl: './radio-button.component.html',
  styleUrl: './radio-button.component.css'
})
export class RadioButtonComponent {

  public isPressed: boolean = false;
  @Input() text: string = "Text";

  public onClick(): void {
    this.isPressed = !this.isPressed;
  }

}

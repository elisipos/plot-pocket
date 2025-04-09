import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

interface btnDetails {
  text: string;
  id?: number;
  isPressed?: boolean;
}

@Component({
  selector: 'app-radio-button',
  imports: [CommonModule],
  templateUrl: './radio-button.component.html',
  styleUrl: './radio-button.component.css'
})
export class RadioButtonComponent implements OnInit {
  
  
  public isPressed: boolean = false;
  public buttons: btnDetails[] = [];
  @Input() text: string[] = [];
  @Input() default: number = 0;
  @Input() generalType: string = '';
  @Output() radioBtnId = new EventEmitter<number>();
  @Output() type = new EventEmitter<string>();
  
  ngOnInit(): void {

    this.type.emit(this.generalType);
    
    let i = 0;
    this.text.forEach(item => {
      this.buttons = [...this.buttons, {text: item, id: i, isPressed: i == this.default ? true : false}];
      i++;
    })
  }


  public onClick(id: number): void {
    // reset the buttons
    this.buttons.forEach(btn => {
      btn.isPressed = false;
    })

    // toggle the correct button
    let i = 0;
    this.buttons.forEach(btn => {
      if(btn.id! == id){
        this.buttons[i].isPressed = !this.buttons[i].isPressed;
        this.radioBtnId.emit(btn.id);
      }
      i++;
    })
  }

}

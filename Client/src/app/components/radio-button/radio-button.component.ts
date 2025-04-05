import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

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
  @Input() text: string[] = [];
  public buttons: btnDetails[] = [];
  
  ngOnInit(): void {
    
    let i = 0;
    this.text.forEach(item => {
      this.buttons = [...this.buttons, {text: item, id: i, isPressed: false}]
      i++;
    })
    console.log(this.buttons)
  }


  public onClick(id: number): void {
    let i = 0;
    this.buttons.forEach(btn => {
      if(btn.id! == id){
        this.buttons[i].isPressed = !this.buttons[i].isPressed;
      }
      i++;
    })
  }

}

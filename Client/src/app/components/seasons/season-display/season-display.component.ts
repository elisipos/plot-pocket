import { Component, Input, OnInit } from '@angular/core';
import { Season } from '../../../models/tvshow/season';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-season-display',
  imports: [CommonModule],
  templateUrl: './season-display.component.html',
  styleUrl: './season-display.component.css'
})
export class SeasonDisplayComponent implements OnInit{

  @Input() seasonsArr!: Season[];
  @Input() inputSeason: Season | null = null;

  ngOnInit(): void {
    this.inputSeason = this.seasonsArr[0];
  }

}

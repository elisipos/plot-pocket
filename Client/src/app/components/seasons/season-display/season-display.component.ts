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

  loadingPoster: boolean = true;
  private _inputSeason: Season | null = null;

  @Input() seasonsArr!: Season[];

  @Input() set inputSeason(value: Season | null) {
    this._inputSeason = value;
    this.loadingPoster = true;
  }

  get inputSeason(): Season | null { 
    return this._inputSeason;
  }

  ngOnInit(): void {
    this.inputSeason = this.seasonsArr[0];
  }

}

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Season } from '../../../models/tvshow/season';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-season-selector',
  imports: [CommonModule],
  templateUrl: './season-selector.component.html',
  styleUrl: './season-selector.component.css'
})
export class SeasonSelectorComponent implements OnInit{

  isOpen: boolean = false;
  selectedSeason!: Season;

  @Input() seasonsArr: Season[] = [];
  @Output() outputSeason = new EventEmitter<Season>();

  setSelectedSeason(season: Season): void {
    this.selectedSeason = season;
    this.outputSeason.emit(this.selectedSeason);
    this.closeDropdown();
  }

  toggleDropdown(): void { this.isOpen = !this.isOpen }

  closeDropdown(): void { this.isOpen = false }

  ngOnInit(): void { this.selectedSeason = this.seasonsArr[0] }

}

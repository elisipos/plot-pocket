import { CommonModule } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MediaFilterService } from '../../services/media-filter.service';

@Component({
  selector: 'app-search-bar',
  imports: [CommonModule, FormsModule],
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.css'
})
export class SearchBarComponent {

  private _mediaFilterService = inject(MediaFilterService);

  public searchQuery: string = '';
  @Input() placeHolder: string = '';

  public search(): void {
    this._mediaFilterService.setSearchQuery(this.searchQuery);
  }

}

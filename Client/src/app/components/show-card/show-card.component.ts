import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MediaItem } from '../../models/media-item';
import { CommonModule } from '@angular/common';
import { User } from '../../auth/models/auth-user';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-show-card',
  imports: [CommonModule, RouterModule],
  templateUrl: './show-card.component.html',
  styleUrl: './show-card.component.css'
})
export class ShowCardComponent {

  @Input() show: MediaItem | null = null;
  @Input() user: User | null = null;
  @Output() showToBookmark = new EventEmitter<MediaItem | null>();

  public toggleBookmark(): void {
    this.showToBookmark.emit(this.show);
  }

  public handleClick(): void {

  }

}

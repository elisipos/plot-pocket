import { Pipe, PipeTransform } from '@angular/core';
import { MediaItem } from '../models/media-item';

@Pipe({
  name: 'nameSearch'
})
export class NameSearchPipe implements PipeTransform {

  transform(
    mediaItemList: MediaItem[] | null,
    searchQuery: string | null
  ): MediaItem[] {
    if(!mediaItemList) {
      return [] as MediaItem[];
    }

    if(!searchQuery?.trim()) {
      return mediaItemList;
    }

    return mediaItemList.filter(item => {
      return item.title?.toLowerCase().includes(searchQuery.toLowerCase());
    })
  }

}

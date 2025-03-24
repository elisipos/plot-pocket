import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MediaListComponent } from './components/media-list/media-list.component';

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule),
  },
  {path: 'media/trending', component: MediaListComponent},
  {path: 'media/movies', component: MediaListComponent},
  {path: 'media/tv-shows', component: MediaListComponent},
  {path: 'media/bookmarks', component: MediaListComponent},
];

export interface MediaItem {
  adult: boolean;
  backdropPath: string;
  id: number;
  title: string;
  originalLanguage: string;
  originalTitle: string;
  overview: string;
  posterPath: string;
  type: string;
  genreIds: number[];
  popularity: number;
  date: string;
  video: boolean;
  voteAverage: number;
  voteCount: number;

  isBookmarked: boolean;
}
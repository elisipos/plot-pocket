export interface MediaItem {
  adult: boolean;
  backdropPath: string;
  id: number;
  title: string;
  originalLanguage: string;
  originalTitle: string;
  overview: string;
  posterPath: string;
  mediaType: string;
  genreIds: number[];
  popularity: number;
  releaseDate: string;
  video: boolean;
  voteAverage: number;
  voteCount: number;

  isBookmarked: boolean;
}
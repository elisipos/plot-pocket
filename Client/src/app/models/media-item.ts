import { Genre } from "./genre";

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
  genres: Genre[];
  popularity: number;
  date: string;
  video: boolean;
  voteAverage: number;
  voteCount: number;
  tagline: string;
  budget: number;
  homepage: string;
  runtime: number;

  showApiId: number;
}
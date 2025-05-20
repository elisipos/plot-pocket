import { Genre } from "./genre";
import { Episode } from "./tvshow/episode";
import { Person } from "./tvshow/person";
import { Season } from "./tvshow/season";

export interface MediaItem {
  adult: boolean;
  backdropPath: string;
  highResBackdropPath: string;
  id: number;
  title: string;
  originalLanguage: string;
  originalTitle: string;
  overview: string;
  posterPath: string;
  highResPosterPath: string;
  type: string;
  genres: Genre[];
  popularity: number;
  date: string;
  video: boolean;
  voteAverage: number;
  voteCount: number;
  homepage: string;

  // Movie Specific
  tagline: string;
  budget: number;
  runtime: number;

  // TvShow Specific
  createdBy: Person[];
  firstAirDate: string;
  inProduction: boolean;
  languages: string[];
  lastAirDate: string;
  lastEpisodeToAir: Episode;
  name: string;
  nextEpisodeToAir: Episode;
  numberOfEpisodes: number;
  numberOfSeasons: number;
  originCountry: string[];
  originalName: string;
  seasons: Season[];

  showApiId: number;
}
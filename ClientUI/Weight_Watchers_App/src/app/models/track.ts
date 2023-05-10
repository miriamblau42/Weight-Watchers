export interface Track {
  id: number;
  date?: Date | string;
  weight: number;
  trend?: string;
  comment?: string;
  bmi: number;
}

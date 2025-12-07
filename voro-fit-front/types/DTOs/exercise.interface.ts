export enum ExerciseTypeEnum {
  Public = 1,
  Custom = 2,
}

export interface ExerciseDto {
  id: string
  name: string
  description?: string
  muscleGroup: string
  type: ExerciseTypeEnum
  thumbnailUrl?: string
  videoUrl?: string
  notes?: string
  alternatives?: string
  createdAt: Date
  updatedAt?: Date
}

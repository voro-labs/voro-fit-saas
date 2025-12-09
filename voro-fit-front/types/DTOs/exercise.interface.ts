import { ExerciseTypeEnum } from "../Enums/exerciseTypeEnum.enum"

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

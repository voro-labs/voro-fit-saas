import type { WorkoutExerciseDto } from "./workout-exercise.interface"
import type { StudentDto } from "./student.interface"

export enum WorkoutStatusEnum {
  Active = 1,
  Inactive = 2,
  Completed = 3,
}

export interface WorkoutHistoryDto {
  id: string
  name: string
  studentId: string
  student?: StudentDto
  status: WorkoutStatusEnum
  completionPercentage?: number
  exercises?: WorkoutExerciseDto[]
  createdAt: Date
  updatedAt?: Date
}

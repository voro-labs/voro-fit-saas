import type { WorkoutExerciseDto } from "./workout-exercise.interface"
import type { StudentDto } from "./student.interface"
import { WorkoutStatusEnum } from "../Enums/workoutStatusEnum.enum"

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

import type { DayOfWeekEnum } from "../Enums/dayOfWeekEnum.enum"
import type { WorkoutPlanExerciseDto } from "./workout-plan-exercise.interface"

export interface WorkoutPlanDayDto {
  id?: string
  workoutPlanWeekId?: string
  dayOfWeek?: DayOfWeekEnum
  exercises?: WorkoutPlanExerciseDto[]
  createdAt?: Date
  updatedAt?: Date
}

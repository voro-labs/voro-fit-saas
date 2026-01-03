import type { DayOfWeekEnum } from "../Enums/dayOfWeekEnum.enum"
import type { WorkoutPlanExerciseDto } from "./workout-plan-exercise.interface"

export interface WorkoutPlanDayDto {
  id?: string | null
  workoutPlanWeekId?: string | null
  dayOfWeek?: DayOfWeekEnum
  time?: string
  exercises?: WorkoutPlanExerciseDto[]
  createdAt?: Date
  updatedAt?: Date
}

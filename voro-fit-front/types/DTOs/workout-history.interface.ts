import type { WorkoutHistoryExerciseDto } from "./workout-history-exercise.interface"

export interface WorkoutHistoryDto {
  id: string
  workoutPlanId: string
  workoutPlanWeekId: string
  workoutPlanDayId: string
  executionDate: Date
  status: "Completed" | "Partial" | "Skipped"
  exercises?: WorkoutHistoryExerciseDto[]
  notes?: string
  createdAt: Date
  updatedAt?: Date
}

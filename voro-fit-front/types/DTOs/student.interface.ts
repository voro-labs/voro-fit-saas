import type { MeasurementDto } from "./measurement.interface"
import type { WorkoutHistoryDto } from "./workout-history.interface"
import type { MealPlanDto } from "./meal-plan.interface"

export enum StudentStatusEnum {
  Active = 1,
  Inactive = 2,
  Pending = 3,
}

export interface StudentDto {
  id: string
  name: string
  email?: string
  phone?: string
  avatarUrl?: string
  birthDate?: Date
  height?: number
  weight?: number
  goal?: string
  notes?: string
  status: StudentStatusEnum
  createdAt: Date
  updatedAt?: Date
  measurements?: MeasurementDto[]
  workoutHistories?: WorkoutHistoryDto[]
  mealPlans?: MealPlanDto[]
}

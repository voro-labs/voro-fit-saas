import type { MeasurementDto } from "./measurement.interface"
import type { WorkoutHistoryDto } from "./workout-history.interface"
import type { MealPlanDto } from "./meal-plan.interface"
import { StudentStatusEnum } from "../Enums/studentStatusEnum.enum"
import { UserExtensionDto } from "./userExtensionDto.interface"

export interface StudentDto {
  userExtensionId: string
  userExtension?: UserExtensionDto
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

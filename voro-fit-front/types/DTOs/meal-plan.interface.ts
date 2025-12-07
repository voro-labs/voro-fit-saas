import type { MealPlanDayDto } from "./meal-plan-day.interface"
import type { StudentDto } from "./student.interface"

export enum MealPlanStatusEnum {
  Active = 1,
  Inactive = 2,
}

export interface MealPlanDto {
  id: string
  studentId: string
  student?: StudentDto
  status: MealPlanStatusEnum
  notes?: string
  days?: MealPlanDayDto[]
  createdAt: Date
  updatedAt?: Date
}

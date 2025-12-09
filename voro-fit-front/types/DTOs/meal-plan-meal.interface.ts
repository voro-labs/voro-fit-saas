import { MealPeriodEnum } from "../Enums/mealPeriodEnum.enum"

export interface MealPlanMealDto {
  id: string
  mealPlanDayId: string
  period: MealPeriodEnum
  time?: string
  description: string
  quantity?: string
  notes?: string
  order: number
}

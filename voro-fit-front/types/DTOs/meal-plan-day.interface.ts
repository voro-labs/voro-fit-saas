import { DayOfWeekEnum } from "../Enums/dayOfWeekEnum.enum"
import type { MealPlanMealDto } from "./meal-plan-meal.interface"

export interface MealPlanDayDto {
  id: string
  mealPlanId: string
  dayOfWeek: DayOfWeekEnum
  meals?: MealPlanMealDto[]
}

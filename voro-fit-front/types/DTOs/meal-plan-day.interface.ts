import type { MealPlanMealDto } from "./meal-plan-meal.interface"

export enum DayOfWeekEnum {
  Segunda = 1,
  Terca = 2,
  Quarta = 3,
  Quinta = 4,
  Sexta = 5,
  Sabado = 6,
  Domingo = 7,
}

export interface MealPlanDayDto {
  id: string
  mealPlanId: string
  dayOfWeek: DayOfWeekEnum
  meals?: MealPlanMealDto[]
}

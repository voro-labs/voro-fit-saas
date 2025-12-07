export enum MealPeriodEnum {
  CafeDaManha = 1,
  LancheDaManha = 2,
  Almoco = 3,
  LancheDaTarde = 4,
  Jantar = 5,
  Ceia = 6,
}

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

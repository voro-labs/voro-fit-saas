"use client"

import type React from "react"

import { useState } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Label } from "@/components/ui/label"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { MealBlock } from "@/components/meal-block"
import { ArrowLeft, Plus, Loader2 } from "lucide-react"
import Link from "next/link"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { useStudents } from "@/hooks/use-students.hook"
import { useMealPlans } from "@/hooks/use-meal-plans.hook"
import { DayOfWeekEnum } from "@/types/DTOs/meal-plan-day.interface"
import { MealPlanStatusEnum } from "@/types/DTOs/meal-plan.interface"
import { MealPeriodEnum } from "@/types/DTOs/meal-plan-meal.interface"
import { AuthGuard } from "@/components/auth/auth.guard"

interface Meal {
  id: string
  time: string
  period: string
  description: string
  quantity?: string
  notes?: string
}

const dayOptions = [
  { value: String(DayOfWeekEnum.Segunda), label: "Segunda-feira" },
  { value: String(DayOfWeekEnum.Terca), label: "Terça-feira" },
  { value: String(DayOfWeekEnum.Quarta), label: "Quarta-feira" },
  { value: String(DayOfWeekEnum.Quinta), label: "Quinta-feira" },
  { value: String(DayOfWeekEnum.Sexta), label: "Sexta-feira" },
  { value: String(DayOfWeekEnum.Sabado), label: "Sábado" },
  { value: String(DayOfWeekEnum.Domingo), label: "Domingo" },
]

const periodToEnum: Record<string, MealPeriodEnum> = {
  "Café da Manhã": MealPeriodEnum.CafeDaManha,
  "Lanche da Manhã": MealPeriodEnum.LancheDaManha,
  Almoço: MealPeriodEnum.Almoco,
  "Lanche da Tarde": MealPeriodEnum.LancheDaTarde,
  Jantar: MealPeriodEnum.Jantar,
  Ceia: MealPeriodEnum.Ceia,
}

export default function NewMealPlanPage() {
  const router = useRouter()
  const { students } = useStudents()
  const { createMealPlan, loading, error } = useMealPlans()
  const [selectedStudentId, setSelectedStudentId] = useState("")
  const [selectedDay, setSelectedDay] = useState(String(DayOfWeekEnum.Segunda))
  const [meals, setMeals] = useState<Meal[]>([])

  const addMeal = () => {
    const newMeal: Meal = {
      id: Math.random().toString(36).substr(2, 9),
      time: "",
      period: "",
      description: "",
      quantity: "",
      notes: "",
    }
    setMeals([...meals, newMeal])
  }

  const removeMeal = (id: string) => {
    setMeals(meals.filter((meal) => meal.id !== id))
  }

  const updateMeal = (id: string, field: keyof Meal, value: string) => {
    setMeals(meals.map((meal) => (meal.id === id ? { ...meal, [field]: value } : meal)))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (!selectedStudentId) return

    const result = await createMealPlan({
      studentId: selectedStudentId,
      status: MealPlanStatusEnum.Active,
      days: [
        {
          id: "",
          mealPlanId: "",
          dayOfWeek: Number(selectedDay) as DayOfWeekEnum,
          meals: meals.map((meal, index) => ({
            id: "",
            mealPlanDayId: "",
            period: periodToEnum[meal.period] || MealPeriodEnum.CafeDaManha,
            time: meal.time,
            description: meal.description,
            quantity: meal.quantity,
            notes: meal.notes,
            order: index,
          })),
        },
      ],
    })

    if (result) {
      router.push("/nutrition")
    }
  }

  return (
    <AuthGuard requiredRoles={["Admin"]}>
      <div className="min-h-screen bg-background p-4 md:p-8">
        <div className="max-w-6xl mx-auto space-y-6">
          <div className="mb-6">
            <Button variant="ghost" asChild className="mb-4">
              <Link href="/nutrition">
                <ArrowLeft className="mr-2 h-4 w-4" />
                Voltar para planos
              </Link>
            </Button>

            <h1 className="text-3xl font-bold text-balance">Novo Plano Alimentar</h1>
            <p className="text-muted-foreground">Crie um plano alimentar personalizado</p>
          </div>

          {error && (
            <div className="mb-6 rounded-lg border border-destructive/50 bg-destructive/10 p-4 text-destructive max-w-4xl">
              {error}
            </div>
          )}

          <Card className="max-w-4xl">
            <CardHeader>
              <CardTitle>Informações do Plano</CardTitle>
              <CardDescription>Selecione o aluno e o dia da semana</CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleSubmit} className="space-y-6">
                <div className="grid gap-4 sm:grid-cols-2">
                  <div className="space-y-2">
                    <Label htmlFor="student">Selecionar Aluno *</Label>
                    <Select value={selectedStudentId} onValueChange={setSelectedStudentId}>
                      <SelectTrigger>
                        <SelectValue placeholder="Escolha um aluno" />
                      </SelectTrigger>
                      <SelectContent>
                        {students.map((student) => (
                          <SelectItem key={student.id} value={student.id}>
                            {student.name}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="day">Dia da Semana *</Label>
                    <Select value={selectedDay} onValueChange={setSelectedDay}>
                      <SelectTrigger>
                        <SelectValue placeholder="Escolha o dia" />
                      </SelectTrigger>
                      <SelectContent>
                        {dayOptions.map((day) => (
                          <SelectItem key={day.value} value={day.value}>
                            {day.label}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  </div>
                </div>

                <div className="space-y-4">
                  <div className="flex items-center justify-between">
                    <h3 className="text-lg font-semibold">Refeições do Dia</h3>
                    <Button type="button" size="sm" onClick={addMeal}>
                      <Plus className="mr-2 h-4 w-4" />
                      Adicionar Refeição
                    </Button>
                  </div>

                  {meals.length === 0 ? (
                    <Card className="border-dashed">
                      <CardContent className="flex flex-col items-center justify-center py-12">
                        <p className="text-muted-foreground">Nenhuma refeição adicionada ainda</p>
                        <p className="text-sm text-muted-foreground">Clique em "Adicionar Refeição" para começar</p>
                      </CardContent>
                    </Card>
                  ) : (
                    <div className="space-y-4">
                      {meals.map((meal) => (
                        <MealBlock
                          key={meal.id}
                          meal={meal}
                          onRemove={() => removeMeal(meal.id)}
                          onChange={(field, value) => updateMeal(meal.id, field, value)}
                        />
                      ))}
                    </div>
                  )}
                </div>

                <div className="flex gap-3 justify-end pt-4 border-t">
                  <Button type="button" variant="outline" asChild>
                    <Link href="/nutrition">Cancelar</Link>
                  </Button>
                  <Button type="submit" disabled={loading || !selectedStudentId}>
                    {loading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                    Salvar Plano
                  </Button>
                </div>
              </form>
            </CardContent>
          </Card>
        </div>
      </div>
    </AuthGuard>
  )
}

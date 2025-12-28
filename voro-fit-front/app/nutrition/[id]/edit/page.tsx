"use client"

import type React from "react"

import { useState, useEffect } from "react"
import { useParams, useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Label } from "@/components/ui/label"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { MealBlock } from "@/components/meal-block"
import { ArrowLeft, Plus, Loader2, UtensilsCrossed, User, Calendar, FileText, Save } from "lucide-react"
import Link from "next/link"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { useStudents } from "@/hooks/use-students.hook"
import { useMealPlans } from "@/hooks/use-meal-plans.hook"
import { DayOfWeekEnum } from "@/types/Enums/dayOfWeekEnum.enum"
import { MealPlanStatusEnum } from "@/types/Enums/mealPlanStatusEnum.enum"
import { MealPeriodEnum } from "@/types/Enums/mealPeriodEnum.enum"
import { AuthGuard } from "@/components/auth/auth.guard"
import { Loading } from "@/components/ui/custom/loading/loading"

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

const periodFromEnum: Record<MealPeriodEnum, string> = {
  [MealPeriodEnum.CafeDaManha]: "Café da Manhã",
  [MealPeriodEnum.LancheDaManha]: "Lanche da Manhã",
  [MealPeriodEnum.Almoco]: "Almoço",
  [MealPeriodEnum.LancheDaTarde]: "Lanche da Tarde",
  [MealPeriodEnum.Jantar]: "Jantar",
  [MealPeriodEnum.Ceia]: "Ceia",
}

export default function EditMealPlanPage() {
  const params = useParams()
  const router = useRouter()
  const { students } = useStudents()
  const { fetchMealPlanById, updateMealPlan, loading, error } = useMealPlans()
  const [selectedStudentId, setSelectedStudentId] = useState("")
  const [selectedDay, setSelectedDay] = useState(String(DayOfWeekEnum.Segunda))
  const [meals, setMeals] = useState<Meal[]>([])
  const [loadingData, setLoadingData] = useState(true)

  useEffect(() => {
    if (params.id) {
      fetchMealPlanById(params.id as string).then((data) => {
        if (data) {
          setSelectedStudentId(data.studentId || "")
          if (data.days && data.days.length > 0) {
            const firstDay = data.days[0]
            setSelectedDay(String(firstDay.dayOfWeek))
            const mealsData: Meal[] =
              firstDay.meals?.map((meal) => ({
                id: meal.id || Math.random().toString(36).substr(2, 9),
                time: meal.time || "",
                period: periodFromEnum[meal.period] || "",
                description: meal.description || "",
                quantity: meal.quantity || "",
                notes: meal.notes || "",
              })) || []
            setMeals(mealsData)
          }
        }
        setLoadingData(false)
      })
    }
  }, [params.id, fetchMealPlanById])

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

    const result = await updateMealPlan(params.id as string, {
      studentId: selectedStudentId,
      status: MealPlanStatusEnum.Active,
      days: [
        {
          id: "",
          mealPlanId: params.id as string,
          dayOfWeek: Number(selectedDay) as DayOfWeekEnum,
          meals: meals.map((meal, index) => ({
            id: meal.id,
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
      router.push(`/nutrition/${params.id}`)
    }
  }

  if (loadingData) {
    return <Loading isLoading={true} />
  }

  return (
    <AuthGuard requiredRoles={["Trainer"]}>
      <div className="min-h-screen bg-linear-to-br from-background via-background to-muted/20">
        <div className="max-w-5xl mx-auto p-4 md:p-8 space-y-6">
          <div className="space-y-4">
            <Button variant="ghost" size="sm" asChild className="group">
              <Link href={`/nutrition/${params.id}`}>
                <ArrowLeft className="mr-2 h-4 w-4 transition-transform group-hover:-translate-x-1" />
                Voltar para detalhes
              </Link>
            </Button>

            <div className="flex items-center gap-4">
              <div className="flex h-14 w-14 items-center justify-center rounded-xl bg-primary/10">
                <UtensilsCrossed className="h-7 w-7 text-primary" />
              </div>
              <div>
                <h1 className="text-3xl font-bold text-balance">Editar Plano Alimentar</h1>
                <p className="text-muted-foreground">Atualize o plano alimentar do aluno</p>
              </div>
            </div>
          </div>

          {error && (
            <div className="rounded-xl border border-destructive/50 bg-destructive/10 p-4 text-destructive animate-in fade-in slide-in-from-top-2">
              <p className="font-medium">{error}</p>
            </div>
          )}

          <Card className="border-border/50 shadow-lg">
            <CardHeader className="space-y-1 pb-6">
              <CardTitle className="text-2xl">Informações do Plano</CardTitle>
              <CardDescription>Edite o aluno e o dia da semana</CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleSubmit} className="space-y-8">
                {/* Basic Information Section */}
                <div className="space-y-6">
                  <div className="flex items-center gap-2 text-sm font-semibold text-primary">
                    <FileText className="h-4 w-4" />
                    <span>Informações Básicas</span>
                  </div>

                  <div className="grid gap-4 sm:grid-cols-2">
                    <div className="space-y-2">
                      <Label htmlFor="student" className="text-base flex items-center gap-2">
                        <User className="h-4 w-4" />
                        Selecionar Aluno *
                      </Label>
                      <Select value={selectedStudentId} onValueChange={setSelectedStudentId}>
                        <SelectTrigger className="h-12 text-base">
                          <SelectValue placeholder="Escolha um aluno" />
                        </SelectTrigger>
                        <SelectContent>
                          {students.map((student) => (
                            <SelectItem key={student.userExtensionId} value={student.userExtensionId}>
                              {student.userExtension?.user.userName}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                    </div>

                    <div className="space-y-2">
                      <Label htmlFor="day" className="text-base flex items-center gap-2">
                        <Calendar className="h-4 w-4" />
                        Dia da Semana *
                      </Label>
                      <Select value={selectedDay} onValueChange={setSelectedDay}>
                        <SelectTrigger className="h-12 text-base">
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
                </div>

                {/* Meals Section */}
                <div className="space-y-4">
                  <div className="flex items-center justify-between">
                    <div className="flex items-center gap-2 text-sm font-semibold text-primary">
                      <UtensilsCrossed className="h-4 w-4" />
                      <span>Refeições do Dia</span>
                    </div>
                    <Button
                      type="button"
                      size="sm"
                      onClick={addMeal}
                      variant="outline"
                      className="gap-2 bg-transparent"
                    >
                      <Plus className="h-4 w-4" />
                      Adicionar Refeição
                    </Button>
                  </div>

                  {meals.length === 0 ? (
                    <Card className="border-2 border-dashed border-border/50 bg-muted/20">
                      <CardContent className="flex flex-col items-center justify-center py-16">
                        <div className="flex h-16 w-16 items-center justify-center rounded-full bg-muted mb-4">
                          <UtensilsCrossed className="h-8 w-8 text-muted-foreground" />
                        </div>
                        <p className="text-base font-medium mb-1">Nenhuma refeição adicionada ainda</p>
                        <p className="text-sm text-muted-foreground mb-4">
                          Clique em "Adicionar Refeição" para começar
                        </p>
                        <Button
                          type="button"
                          size="sm"
                          onClick={addMeal}
                          variant="outline"
                          className="gap-2 bg-transparent"
                        >
                          <Plus className="h-4 w-4" />
                          Adicionar Primeira Refeição
                        </Button>
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

                <div className="flex gap-3 justify-end pt-6 border-t">
                  <Button type="button" variant="outline" size="lg" asChild>
                    <Link href={`/nutrition/${params.id}`}>Cancelar</Link>
                  </Button>
                  <Button type="submit" size="lg" disabled={loading || !selectedStudentId} className="min-w-[180px]">
                    {loading ? (
                      <>
                        <Loader2 className="mr-2 h-5 w-5 animate-spin" />
                        Salvando...
                      </>
                    ) : (
                      <>
                        <Save className="mr-2 h-5 w-5" />
                        Salvar Alterações
                      </>
                    )}
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

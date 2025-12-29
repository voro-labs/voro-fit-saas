"use client"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Plus, Calendar, ChevronDown, ChevronUp } from "lucide-react"
import { WorkoutDayBlock } from "./workout-day-block"
import type { WorkoutPlanWeekDto } from "@/types/DTOs/workout-plan-week.interface"
import type { WorkoutPlanDayDto } from "@/types/DTOs/workout-plan-day.interface"
import { DayOfWeekEnum } from "@/types/Enums/dayOfWeekEnum.enum"

interface WorkoutWeekBlockProps {
  week: WorkoutPlanWeekDto
  weekIndex: number
  onRemove: () => void
  onChange: (days: WorkoutPlanDayDto[]) => void
}

const dayOptions = [
  { value: DayOfWeekEnum.Segunda, label: "Segunda-feira" },
  { value: DayOfWeekEnum.Terca, label: "Terça-feira" },
  { value: DayOfWeekEnum.Quarta, label: "Quarta-feira" },
  { value: DayOfWeekEnum.Quinta, label: "Quinta-feira" },
  { value: DayOfWeekEnum.Sexta, label: "Sexta-feira" },
  { value: DayOfWeekEnum.Sabado, label: "Sábado" },
  { value: DayOfWeekEnum.Domingo, label: "Domingo" },
]

export function WorkoutWeekBlock({ week, weekIndex, onRemove, onChange }: WorkoutWeekBlockProps) {
  const [isExpanded, setIsExpanded] = useState(true)
  const days = week.days || []

  const addDay = () => {
    const usedDays = days.map((d) => d.dayOfWeek)
    const availableDay = dayOptions.find((d) => !usedDays.includes(d.value))

    if (!availableDay) {
      alert("Todos os dias da semana já foram adicionados")
      return
    }

    const newDay: WorkoutPlanDayDto = {
      id: Math.random().toString(36).substr(2, 9),
      workoutPlanWeekId: week.id || "",
      dayOfWeek: availableDay.value,
      exercises: [],
    }
    onChange([...days, newDay])
  }

  const removeDay = (dayIndex: number) => {
    onChange(days.filter((_, i) => i !== dayIndex))
  }

  const updateDay = (dayIndex: number, updatedDay: WorkoutPlanDayDto) => {
    const updated = [...days]
    updated[dayIndex] = updatedDay
    onChange(updated)
  }

  return (
    <Card className="border-2 border-primary/20">
      <CardHeader className="pb-3">
        <div className="flex items-center justify-between">
          <div className="flex items-center gap-3">
            <Button variant="ghost" size="icon" onClick={() => setIsExpanded(!isExpanded)} className="h-8 w-8">
              {isExpanded ? <ChevronUp className="h-4 w-4" /> : <ChevronDown className="h-4 w-4" />}
            </Button>
            <CardTitle className="text-lg flex items-center gap-2">
              <Calendar className="h-5 w-5 text-primary" />
              Semana {weekIndex + 1}
            </CardTitle>
            <span className="text-sm text-muted-foreground">
              ({days.length} {days.length === 1 ? "dia" : "dias"})
            </span>
          </div>
          <div className="flex items-center gap-2">
            <Button type="button" size="sm" onClick={addDay} variant="outline">
              <Plus className="h-4 w-4 mr-2" />
              Adicionar Dia
            </Button>
            <Button type="button" size="sm" onClick={onRemove} variant="destructive">
              Remover Semana
            </Button>
          </div>
        </div>
      </CardHeader>

      {isExpanded && (
        <CardContent className="space-y-4">
          {days.length === 0 ? (
            <Card className="border-2 border-dashed border-border/50 bg-muted/20">
              <CardContent className="flex flex-col items-center justify-center py-12">
                <Calendar className="h-12 w-12 text-muted-foreground mb-3" />
                <p className="text-base font-medium mb-1">Nenhum dia adicionado ainda</p>
                <p className="text-sm text-muted-foreground mb-4">Adicione dias de treino para esta semana</p>
                <Button type="button" size="sm" onClick={addDay} variant="outline">
                  <Plus className="h-4 w-4 mr-2" />
                  Adicionar Primeiro Dia
                </Button>
              </CardContent>
            </Card>
          ) : (
            days.map((day, dayIndex) => (
              <WorkoutDayBlock
                key={day.id || dayIndex}
                day={day}
                onRemove={() => removeDay(dayIndex)}
                onChange={(updatedDay) => updateDay(dayIndex, updatedDay)}
              />
            ))
          )}
        </CardContent>
      )}
    </Card>
  )
}

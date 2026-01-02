"use client"

import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Label } from "@/components/ui/label"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Calendar, Dumbbell } from "lucide-react"
import { WorkoutExerciseBlock } from "./workout-exercise-block"
import type { WorkoutPlanDayDto } from "@/types/DTOs/workout-plan-day.interface"
import type { WorkoutPlanExerciseDto } from "@/types/DTOs/workout-plan-exercise.interface"
import { DayOfWeekEnum } from "@/types/Enums/dayOfWeekEnum.enum"

interface WorkoutDayBlockProps {
  day: WorkoutPlanDayDto
  onRemove: () => void
  onChange: (day: WorkoutPlanDayDto) => void
}

const dayOptions = [
  { value: String(DayOfWeekEnum.Monday), label: "Segunda-feira" },
  { value: String(DayOfWeekEnum.Tuesday), label: "Terça-feira" },
  { value: String(DayOfWeekEnum.Wednesday), label: "Quarta-feira" },
  { value: String(DayOfWeekEnum.Thursday), label: "Quinta-feira" },
  { value: String(DayOfWeekEnum.Friday), label: "Sexta-feira" },
  { value: String(DayOfWeekEnum.Saturday), label: "Sábado" },
  { value: String(DayOfWeekEnum.Sunday), label: "Domingo" },
]

export function WorkoutDayBlock({ day, onRemove, onChange }: WorkoutDayBlockProps) {
  const exercises = day.exercises || []

  const updateExercises = (newExercises: WorkoutPlanExerciseDto[]) => {
    onChange({ ...day, exercises: newExercises })
  }

  return (
    <Card className="bg-muted/30">
      <CardHeader className="pb-3">
        <div className="flex items-center justify-between gap-2">
          <CardTitle className="text-base flex items-center gap-2">
            <Calendar className="h-4 w-4 text-primary" />
            Dia de Treino
          </CardTitle>
          <Button type="button" size="sm" onClick={onRemove} variant="destructive">
            Remover Dia
          </Button>
        </div>
      </CardHeader>
      <CardContent className="space-y-4">
        <div className="space-y-2">
          <Label className="flex items-center gap-2">
            <Calendar className="h-4 w-4" />
            Dia da Semana *
          </Label>
          <Select
            value={String(day.dayOfWeek || "")}
            onValueChange={(value) => onChange({ ...day, dayOfWeek: Number(value) as DayOfWeekEnum })}
          >
            <SelectTrigger className="h-12 text-base">
              <SelectValue placeholder="Escolha o dia" />
            </SelectTrigger>
            <SelectContent>
              {dayOptions.map((option) => (
                <SelectItem key={option.value} value={option.value}>
                  {option.label}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>

        <div className="space-y-3">
          <div className="flex items-center gap-2 text-sm font-semibold text-primary">
            <Dumbbell className="h-4 w-4" />
            <span>Exercícios do Dia</span>
          </div>
          <WorkoutExerciseBlock exercises={exercises} onExercisesChange={updateExercises} />
        </div>
      </CardContent>
    </Card>
  )
}

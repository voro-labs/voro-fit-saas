"use client"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { GripVertical, Plus, X, Loader2 } from "lucide-react"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"
import { Badge } from "@/components/ui/badge"
import { useExercises } from "@/hooks/use-exercises.hook"
import type { WorkoutExerciseDto } from "@/types/DTOs/workout-exercise.interface"

interface WorkoutFormProps {
  exercises?: Partial<WorkoutExerciseDto>[]
  onExercisesChange?: (exercises: Partial<WorkoutExerciseDto>[]) => void
}

export function WorkoutForm({ exercises = [], onExercisesChange }: WorkoutFormProps) {
  const { exercises: availableExercises, loading } = useExercises()
  const [isDialogOpen, setIsDialogOpen] = useState(false)

  const addExercise = (exercise: (typeof availableExercises)[0]) => {
    const newExercise: Partial<WorkoutExerciseDto> = {
      exerciseId: exercise.id,
      exercise: exercise,
      sets: 0,
      reps: 0,
      weight: 0,
      restSeconds: 0,
      notes: "",
      alternative: "",
      order: exercises.length,
    }
    onExercisesChange?.([...exercises, newExercise])
    setIsDialogOpen(false)
  }

  const removeExercise = (index: number) => {
    const updated = exercises.filter((_, i) => i !== index).map((ex, i) => ({ ...ex, order: i }))
    onExercisesChange?.(updated)
  }

  const updateExercise = (index: number, field: keyof WorkoutExerciseDto, value: string | number) => {
    const updated = [...exercises]
    updated[index] = { ...updated[index], [field]: value }
    onExercisesChange?.(updated)
  }

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h3 className="text-lg font-semibold">Exercícios</h3>
        <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
          <DialogTrigger asChild>
            <Button size="sm">
              <Plus className="mr-2 h-4 w-4" />
              Adicionar Exercício
            </Button>
          </DialogTrigger>
          <DialogContent className="max-w-2xl max-h-[80vh] overflow-y-auto">
            <DialogHeader>
              <DialogTitle>Selecionar Exercício</DialogTitle>
              <DialogDescription>Escolha um exercício da biblioteca</DialogDescription>
            </DialogHeader>
            {loading ? (
              <div className="flex items-center justify-center py-8">
                <Loader2 className="h-6 w-6 animate-spin text-muted-foreground" />
              </div>
            ) : availableExercises.length === 0 ? (
              <div className="flex items-center justify-center py-8">
                <p className="text-muted-foreground">Nenhum exercício cadastrado</p>
              </div>
            ) : (
              <div className="grid gap-3 sm:grid-cols-2">
                {availableExercises.map((exercise) => (
                  <Card
                    key={exercise.id}
                    className="cursor-pointer hover:border-primary transition-colors"
                    onClick={() => addExercise(exercise)}
                  >
                    <CardContent className="p-4">
                      <h4 className="font-medium text-balance">{exercise.name}</h4>
                      <p className="text-sm text-muted-foreground mt-1">{exercise.muscleGroup}</p>
                    </CardContent>
                  </Card>
                ))}
              </div>
            )}
          </DialogContent>
        </Dialog>
      </div>

      {exercises.length === 0 ? (
        <Card className="border-dashed">
          <CardContent className="flex flex-col items-center justify-center py-12">
            <p className="text-muted-foreground">Nenhum exercício adicionado ainda</p>
            <p className="text-sm text-muted-foreground">Clique em "Adicionar Exercício" para começar</p>
          </CardContent>
        </Card>
      ) : (
        <div className="space-y-4">
          {exercises.map((exercise, index) => (
            <Card key={index}>
              <CardHeader className="pb-3">
                <div className="flex items-start justify-between gap-2">
                  <div className="flex items-start gap-3 flex-1">
                    <div className="mt-1 cursor-grab">
                      <GripVertical className="h-5 w-5 text-muted-foreground" />
                    </div>
                    <div className="flex-1">
                      <CardTitle className="text-base">{exercise.exercise?.name || "Exercício"}</CardTitle>
                      <Badge variant="secondary" className="mt-1">
                        {exercise.exercise?.muscleGroup || "-"}
                      </Badge>
                    </div>
                  </div>
                  <Button variant="ghost" size="icon" onClick={() => removeExercise(index)}>
                    <X className="h-4 w-4" />
                  </Button>
                </div>
              </CardHeader>
              <CardContent className="space-y-4">
                <div className="grid gap-4 sm:grid-cols-4">
                  <div className="space-y-2">
                    <Label htmlFor={`sets-${index}`}>Séries</Label>
                    <Input
                      id={`sets-${index}`}
                      type="number"
                      placeholder="Ex: 4"
                      value={exercise.sets || ""}
                      onChange={(e) => updateExercise(index, "sets", Number(e.target.value))}
                    />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor={`reps-${index}`}>Repetições</Label>
                    <Input
                      id={`reps-${index}`}
                      type="number"
                      placeholder="Ex: 12"
                      value={exercise.reps || ""}
                      onChange={(e) => updateExercise(index, "reps", Number(e.target.value))}
                    />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor={`weight-${index}`}>Peso (kg)</Label>
                    <Input
                      id={`weight-${index}`}
                      type="number"
                      placeholder="Ex: 80"
                      value={exercise.weight || ""}
                      onChange={(e) => updateExercise(index, "weight", Number(e.target.value))}
                    />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor={`rest-${index}`}>Descanso (s)</Label>
                    <Input
                      id={`rest-${index}`}
                      type="number"
                      placeholder="Ex: 90"
                      value={exercise.restSeconds || ""}
                      onChange={(e) => updateExercise(index, "restSeconds", Number(e.target.value))}
                    />
                  </div>
                </div>

                <div className="space-y-2">
                  <Label htmlFor={`notes-${index}`}>Observações</Label>
                  <Textarea
                    id={`notes-${index}`}
                    placeholder="Técnicas, cuidados especiais..."
                    rows={2}
                    value={exercise.notes || ""}
                    onChange={(e) => updateExercise(index, "notes", e.target.value)}
                  />
                </div>

                <div className="space-y-2">
                  <Label htmlFor={`alternative-${index}`}>Exercício Alternativo</Label>
                  <Input
                    id={`alternative-${index}`}
                    placeholder="Ex: Supino com halteres"
                    value={exercise.alternative || ""}
                    onChange={(e) => updateExercise(index, "alternative", e.target.value)}
                  />
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      )}
    </div>
  )
}

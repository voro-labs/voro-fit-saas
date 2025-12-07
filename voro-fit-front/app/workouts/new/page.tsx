"use client"

import type React from "react"

import { useState } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Label } from "@/components/ui/label"
import { Input } from "@/components/ui/input"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { WorkoutForm } from "@/components/workout-form"
import { ArrowLeft, Loader2 } from "lucide-react"
import Link from "next/link"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { useStudents } from "@/hooks/use-students.hook"
import { useWorkouts } from "@/hooks/use-workouts.hook"
import { WorkoutStatusEnum } from "@/types/DTOs/workout-history.interface"
import type { WorkoutExerciseDto } from "@/types/DTOs/workout-exercise.interface"
import { AuthGuard } from "@/components/auth/auth.guard"

export default function NewWorkoutPage() {
  const router = useRouter()
  const { students } = useStudents()
  const { createWorkout, loading, error } = useWorkouts()
  const [selectedStudentId, setSelectedStudentId] = useState("")
  const [workoutName, setWorkoutName] = useState("")
  const [exercises, setExercises] = useState<Partial<WorkoutExerciseDto>[]>([])

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (!selectedStudentId || !workoutName) return

    const result = await createWorkout({
      name: workoutName,
      studentId: selectedStudentId,
      status: WorkoutStatusEnum.Active,
      exercises: exercises as WorkoutExerciseDto[],
    })

    if (result) {
      router.push("/workouts")
    }
  }

  return (
    <AuthGuard requiredRoles={["Admin"]}>
      <div className="min-h-screen bg-background p-4 md:p-8">
        <div className="max-w-6xl mx-auto space-y-6">
          <div className="mb-6">
            <Button variant="ghost" asChild className="mb-4">
              <Link href="/workouts">
                <ArrowLeft className="mr-2 h-4 w-4" />
                Voltar para treinos
              </Link>
            </Button>

            <h1 className="text-3xl font-bold text-balance">Montar Treino</h1>
            <p className="text-muted-foreground">Crie um novo treino personalizado para seu aluno</p>
          </div>

          {error && (
            <div className="mb-6 rounded-lg border border-destructive/50 bg-destructive/10 p-4 text-destructive max-w-4xl">
              {error}
            </div>
          )}

          <Card className="max-w-4xl">
            <CardHeader>
              <CardTitle>Informações do Treino</CardTitle>
              <CardDescription>Selecione o aluno e monte o treino</CardDescription>
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
                    <Label htmlFor="name">Nome do Treino *</Label>
                    <Input
                      id="name"
                      placeholder="Ex: Treino A - Peito e Tríceps"
                      value={workoutName}
                      onChange={(e) => setWorkoutName(e.target.value)}
                      required
                    />
                  </div>
                </div>

                <WorkoutForm exercises={exercises} onExercisesChange={setExercises} />

                <div className="flex gap-3 justify-end pt-4 border-t">
                  <Button type="button" variant="outline" asChild>
                    <Link href="/workouts">Cancelar</Link>
                  </Button>
                  <Button type="submit" disabled={loading || !selectedStudentId || !workoutName}>
                    {loading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                    Salvar Treino
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

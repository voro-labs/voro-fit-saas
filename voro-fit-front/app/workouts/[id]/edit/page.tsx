"use client"

import type React from "react"

import { useState, useEffect } from "react"
import { useParams, useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Label } from "@/components/ui/label"
import { Input } from "@/components/ui/input"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { WorkoutForm } from "@/components/workout-form"
import { ArrowLeft, Loader2, Dumbbell, User, FileText, Save } from "lucide-react"
import Link from "next/link"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { useStudents } from "@/hooks/use-students.hook"
import { useWorkouts } from "@/hooks/use-workouts.hook"
import type { WorkoutExerciseDto } from "@/types/DTOs/workout-exercise.interface"
import { AuthGuard } from "@/components/auth/auth.guard"
import { WorkoutStatusEnum } from "@/types/Enums/workoutStatusEnum.enum"
import { Loading } from "@/components/ui/custom/loading/loading"

export default function EditWorkoutPage() {
  const params = useParams()
  const router = useRouter()
  const { students } = useStudents()
  const { fetchWorkoutById, updateWorkout, loading, error } = useWorkouts()
  const [selectedStudentId, setSelectedStudentId] = useState("")
  const [workoutName, setWorkoutName] = useState("")
  const [exercises, setExercises] = useState<Partial<WorkoutExerciseDto>[]>([])
  const [loadingData, setLoadingData] = useState(true)

  useEffect(() => {
    if (params.id) {
      fetchWorkoutById(params.id as string).then((data) => {
        if (data) {
          setWorkoutName(data.name || "")
          setSelectedStudentId(data.studentId || "")
          setExercises(data.exercises || [])
        }
        setLoadingData(false)
      })
    }
  }, [params.id, fetchWorkoutById])

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (!selectedStudentId || !workoutName) return

    const result = await updateWorkout(params.id as string, {
      name: workoutName,
      studentId: selectedStudentId,
      status: WorkoutStatusEnum.Active,
      exercises: exercises as WorkoutExerciseDto[],
    })

    if (result) {
      router.push(`/workouts/${params.id}`)
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
              <Link href={`/workouts/${params.id}`}>
                <ArrowLeft className="mr-2 h-4 w-4 transition-transform group-hover:-translate-x-1" />
                Voltar para detalhes
              </Link>
            </Button>

            <div className="flex items-center gap-4">
              <div className="flex h-14 w-14 items-center justify-center rounded-xl bg-primary/10">
                <Dumbbell className="h-7 w-7 text-primary" />
              </div>
              <div>
                <h1 className="text-3xl font-bold text-balance">Editar Treino</h1>
                <p className="text-muted-foreground">Atualize o treino do aluno</p>
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
              <CardTitle className="text-2xl">Informações do Treino</CardTitle>
              <CardDescription>Edite o aluno e os exercícios do treino</CardDescription>
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
                              {student.userExtension?.user.firstName}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                    </div>

                    <div className="space-y-2">
                      <Label htmlFor="name" className="text-base flex items-center gap-2">
                        <Dumbbell className="h-4 w-4" />
                        Nome do Treino *
                      </Label>
                      <Input
                        id="name"
                        placeholder="Ex: Treino A - Peito e Tríceps"
                        value={workoutName}
                        onChange={(e) => setWorkoutName(e.target.value)}
                        required
                        className="h-12 text-base"
                      />
                    </div>
                  </div>
                </div>

                {/* Exercises Section */}
                <div className="space-y-4">
                  <div className="flex items-center gap-2 text-sm font-semibold text-primary">
                    <Dumbbell className="h-4 w-4" />
                    <span>Exercícios do Treino</span>
                  </div>

                  <WorkoutForm exercises={exercises} onExercisesChange={setExercises} />
                </div>

                <div className="flex gap-3 justify-end pt-6 border-t">
                  <Button type="button" variant="outline" size="lg" asChild>
                    <Link href={`/workouts/${params.id}`}>Cancelar</Link>
                  </Button>
                  <Button
                    type="submit"
                    size="lg"
                    disabled={loading || !selectedStudentId || !workoutName}
                    className="min-w-[180px]"
                  >
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

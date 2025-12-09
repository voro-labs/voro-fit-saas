"use client"

import { useEffect, useState } from "react"
import { useParams } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { ArrowLeft, Calendar, Edit, User, Loader2 } from "lucide-react"
import Link from "next/link"
import { useWorkouts } from "@/hooks/use-workouts.hook"
import { AuthGuard } from "@/components/auth/auth.guard"
import { WorkoutHistoryDto } from "@/types/DTOs/workout-history.interface"
import { WorkoutStatusEnum } from "@/types/Enums/workoutStatusEnum.enum"
import { Loading } from "@/components/ui/custom/loading/loading"

export default function WorkoutDetailPage() {
  const params = useParams()
  const { fetchWorkoutById, loading, error } = useWorkouts()
  const [workout, setWorkout] = useState<WorkoutHistoryDto | null>(null)

  useEffect(() => {
    if (params.id) {
      fetchWorkoutById(params.id as string).then((data) => {
        if (data) setWorkout(data)
      })
    }
  }, [params.id, fetchWorkoutById])

  const formatDate = (date?: Date) => {
    if (!date) return "-"
    return new Date(date).toLocaleDateString("pt-BR")
  }

  if (error) {
    return (
      <div className="flex h-screen">
        <div className="flex flex-1 flex-col overflow-hidden">
          <div className="flex-1 p-6">
            <Button variant="ghost" asChild className="mb-4">
              <Link href="/workouts">
                <ArrowLeft className="mr-2 h-4 w-4" />
                Voltar para treinos
              </Link>
            </Button>
            <div className="rounded-lg border border-destructive/50 bg-destructive/10 p-4 text-destructive">
              {error || "Treino não encontrado"}
            </div>
          </div>
        </div>
      </div>
    )
  }

  return (
    <AuthGuard requiredRoles={["Trainer"]}>
      <Loading isLoading={loading}></Loading>
      <div className="min-h-screen bg-background p-4 md:p-8">
        <div className="max-w-6xl mx-auto space-y-6">
          <div className="mb-6">
            <Button variant="ghost" asChild className="mb-4">
              <Link href="/workouts">
                <ArrowLeft className="mr-2 h-4 w-4" />
                Voltar para treinos
              </Link>
            </Button>
          </div>

          {/* Header */}
          <Card className="mb-6">
            <CardContent className="p-6">
              <div className="flex flex-col gap-6 md:flex-row md:items-start md:justify-between">
                <div className="space-y-3">
                  <div className="flex items-center gap-3 flex-wrap">
                    <h1 className="text-2xl font-bold text-balance">{workout?.name}</h1>
                    <Badge
                      className={
                        workout?.status === WorkoutStatusEnum.Active
                          ? "bg-accent text-accent-foreground"
                          : "bg-muted text-muted-foreground"
                      }
                    >
                      {workout?.status === WorkoutStatusEnum.Active
                        ? "Ativo"
                        : workout?.status === WorkoutStatusEnum.Completed
                          ? "Concluído"
                          : "Inativo"}
                    </Badge>
                  </div>

                  {workout?.student && workout?.student.userExtension?.user && (
                    <div className="flex items-center gap-3">
                      <Avatar className="h-12 w-12">
                        <AvatarImage src={workout?.student.userExtension?.user.avatarUrl || "/placeholder.svg"} alt={`${workout?.student.userExtension?.user.firstName}`} />
                        <AvatarFallback>
                          {`${workout?.student.userExtension?.user.firstName}`
                            .split(" ")
                            .map((n) => n[0])
                            .join("")}
                        </AvatarFallback>
                      </Avatar>
                      <div>
                        <p className="font-medium">{`${workout?.student.userExtension?.user.firstName}`}</p>
                        <p className="text-sm text-muted-foreground">{workout?.exercises?.length || 0} exercícios</p>
                      </div>
                    </div>
                  )}

                  <div className="flex flex-wrap gap-4 text-sm text-muted-foreground">
                    <div className="flex items-center gap-2">
                      <Calendar className="h-4 w-4" />
                      Criado em {formatDate(workout?.createdAt)}
                    </div>
                    {workout?.updatedAt && (
                      <div className="flex items-center gap-2">
                        <Calendar className="h-4 w-4" />
                        Atualizado {formatDate(workout?.updatedAt)}
                      </div>
                    )}
                  </div>
                </div>

                <div className="flex gap-2">
                  {workout?.student && (
                    <Button asChild variant="outline">
                      <Link href={`/students/${workout?.studentId}`}>
                        <User className="mr-2 h-4 w-4" />
                        Ver Aluno
                      </Link>
                    </Button>
                  )}
                  <Button asChild>
                    <Link href={`/workouts/${workout?.id}/edit`}>
                      <Edit className="mr-2 h-4 w-4" />
                      Editar
                    </Link>
                  </Button>
                </div>
              </div>
            </CardContent>
          </Card>

          {/* Exercises List */}
          <div className="space-y-4 max-w-4xl">
            {workout?.exercises && workout?.exercises.length > 0 ? (
              workout?.exercises
                .sort((a, b) => a.order - b.order)
                .map((workoutExercise, index) => (
                  <Card key={workoutExercise.id}>
                    <CardHeader className="pb-3">
                      <div className="flex items-start justify-between gap-2">
                        <div className="space-y-1">
                          <CardTitle className="text-base flex items-center gap-2">
                            <span className="flex h-6 w-6 items-center justify-center rounded-full bg-primary text-xs text-primary-foreground font-bold">
                              {index + 1}
                            </span>
                            {workoutExercise.exercise?.name || "Exercício"}
                          </CardTitle>
                          <Badge variant="secondary">{workoutExercise.exercise?.muscleGroup || "-"}</Badge>
                        </div>
                      </div>
                    </CardHeader>
                    <CardContent className="space-y-3">
                      <div className="grid grid-cols-4 gap-4">
                        <div className="rounded-lg border p-3">
                          <p className="text-xs text-muted-foreground mb-1">Séries</p>
                          <p className="text-lg font-bold">{workoutExercise.sets}</p>
                        </div>
                        <div className="rounded-lg border p-3">
                          <p className="text-xs text-muted-foreground mb-1">Repetições</p>
                          <p className="text-lg font-bold">{workoutExercise.reps}</p>
                        </div>
                        <div className="rounded-lg border p-3">
                          <p className="text-xs text-muted-foreground mb-1">Peso</p>
                          <p className="text-lg font-bold">
                            {workoutExercise.weight ? `${workoutExercise.weight}kg` : "-"}
                          </p>
                        </div>
                        <div className="rounded-lg border p-3">
                          <p className="text-xs text-muted-foreground mb-1">Descanso</p>
                          <p className="text-lg font-bold">
                            {workoutExercise.restSeconds ? `${workoutExercise.restSeconds}s` : "-"}
                          </p>
                        </div>
                      </div>

                      {workoutExercise.notes && (
                        <div className="rounded-lg bg-muted/50 p-3">
                          <p className="text-xs font-medium text-muted-foreground mb-1">Observações</p>
                          <p className="text-sm leading-relaxed">{workoutExercise.notes}</p>
                        </div>
                      )}

                      {workoutExercise.alternative && (
                        <div className="rounded-lg bg-muted/50 p-3">
                          <p className="text-xs font-medium text-muted-foreground mb-1">Alternativa</p>
                          <p className="text-sm">{workoutExercise.alternative}</p>
                        </div>
                      )}
                    </CardContent>
                  </Card>
                ))
            ) : (
              <Card className="border-dashed">
                <CardContent className="flex flex-col items-center justify-center py-12">
                  <p className="text-muted-foreground">Nenhum exercício adicionado a este treino</p>
                </CardContent>
              </Card>
            )}
          </div>
        </div>
      </div>
    </AuthGuard>
  )
}

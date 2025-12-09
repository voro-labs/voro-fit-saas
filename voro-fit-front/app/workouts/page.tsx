"use client"

import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Calendar, Plus, Eye, Loader2 } from "lucide-react"
import Link from "next/link"
import { useWorkouts } from "@/hooks/use-workouts.hook"
import { AuthGuard } from "@/components/auth/auth.guard"
import { WorkoutStatusEnum } from "@/types/Enums/workoutStatusEnum.enum"

export default function WorkoutsPage() {
  const { workouts, loading, error } = useWorkouts()

  const formatDate = (date?: Date) => {
    if (!date) return "-"
    const now = new Date()
    const d = new Date(date)
    const diffDays = Math.floor((now.getTime() - d.getTime()) / (1000 * 60 * 60 * 24))

    if (diffDays === 0) return "Hoje"
    if (diffDays === 1) return "Ontem"
    if (diffDays < 7) return `${diffDays} dias atrás`
    return d.toLocaleDateString("pt-BR")
  }

  return (
    <AuthGuard requiredRoles={["Trainer"]}>
      <div className="min-h-screen bg-background p-4 md:p-8">
        <div className="max-w-6xl mx-auto space-y-6">
          <div className="mb-6 flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
            <div>
              <h1 className="text-3xl font-bold text-balance">Treinos</h1>
              <p className="text-muted-foreground">Gerencie e crie treinos para seus alunos</p>
            </div>
            <Button asChild>
              <Link href="/workouts/new">
                <Plus className="mr-2 h-4 w-4" />
                Novo Treino
              </Link>
            </Button>
          </div>

          {error && (
            <div className="mb-6 rounded-lg border border-destructive/50 bg-destructive/10 p-4 text-destructive">
              {error}
            </div>
          )}

          {loading ? (
            <div className="flex items-center justify-center py-12">
              <Loader2 className="h-8 w-8 animate-spin text-muted-foreground" />
            </div>
          ) : workouts.length === 0 ? (
            <div className="flex flex-col items-center justify-center py-12 text-center">
              <p className="text-muted-foreground mb-4">Nenhum treino cadastrado</p>
              <Button asChild>
                <Link href="/workouts/new">
                  <Plus className="mr-2 h-4 w-4" />
                  Criar primeiro treino
                </Link>
              </Button>
            </div>
          ) : (
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
              {workouts.map((workout) => (
                <Card key={workout.id} className="hover:shadow-md transition-shadow">
                  <CardHeader className="pb-3">
                    <div className="flex items-start justify-between gap-2">
                      <CardTitle className="text-base text-balance leading-tight">{workout.name}</CardTitle>
                      <Badge
                        className={
                          workout.status === WorkoutStatusEnum.Active
                            ? "bg-accent text-accent-foreground"
                            : "bg-muted text-muted-foreground"
                        }
                      >
                        {workout.status === WorkoutStatusEnum.Active
                          ? "Ativo"
                          : workout.status === WorkoutStatusEnum.Completed
                            ? "Concluído"
                            : "Inativo"}
                      </Badge>
                    </div>
                  </CardHeader>
                  <CardContent className="space-y-4">
                    {workout.student && (
                      <div className="flex items-center gap-3">
                        <Avatar className="h-10 w-10">
                          <AvatarImage
                            src={workout.student.userExtension?.user.avatarUrl || "/placeholder.svg"}
                            alt={`${workout.student.userExtension?.user.firstName}`}
                          />
                          <AvatarFallback>
                            {`${workout.student.userExtension?.user.firstName}`
                              .split(" ")
                              .map((n) => n[0])
                              .join("")}
                          </AvatarFallback>
                        </Avatar>
                        <div className="flex-1 min-w-0">
                          <p className="font-medium text-sm truncate">{`${workout.student.userExtension?.user.firstName}`}</p>
                          <p className="text-xs text-muted-foreground">{workout.exercises?.length || 0} exercícios</p>
                        </div>
                      </div>
                    )}

                    <div className="flex items-center justify-between text-xs text-muted-foreground">
                      <div className="flex items-center gap-1">
                        <Calendar className="h-3 w-3" />
                        {formatDate(workout.updatedAt || workout.createdAt)}
                      </div>
                    </div>

                    <Button asChild variant="outline" size="sm" className="w-full bg-transparent">
                      <Link href={`/workouts/${workout.id}`}>
                        <Eye className="h-4 w-4 mr-2" />
                        Ver Detalhes
                      </Link>
                    </Button>
                  </CardContent>
                </Card>
              ))}
            </div>
          )}
        </div>
      </div>
    </AuthGuard>
  )
}

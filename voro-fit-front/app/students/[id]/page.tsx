"use client"

import { useEffect, useState } from "react"
import { useParams } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Progress } from "@/components/ui/progress"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import {
  ArrowLeft,
  Calendar,
  Dumbbell,
  FileText,
  Mail,
  MessageSquare,
  Phone,
  TrendingUp,
  Edit,
  Loader2,
} from "lucide-react"
import Link from "next/link"
import { useStudents } from "@/hooks/use-students.hook"
import { AuthGuard } from "@/components/auth/auth.guard"
import { StudentStatusEnum } from "@/types/Enums/studentStatusEnum.enum"
import { StudentDto } from "@/types/DTOs/student.interface"
import { Loading } from "@/components/ui/custom/loading/loading"
import { DayOfWeekEnum } from "@/types/Enums/dayOfWeekEnum.enum"

export default function StudentDetailPage() {
  const params = useParams()
  const { fetchStudentById, loading, error } = useStudents()
  const [student, setStudent] = useState<StudentDto | null>(null)

  useEffect(() => {
    if (params.id) {
      fetchStudentById(params.id as string).then((data) => {
        if (data) setStudent(data)
      })
    }
  }, [params.id, fetchStudentById])

  const statusConfig = {
    [StudentStatusEnum.Unspecified]: { label: "Não definido", color: "bg-muted text-muted-foreground", },
    [StudentStatusEnum.Active]: { label: "Ativo", color: "bg-accent text-accent-foreground" },
    [StudentStatusEnum.Inactive]: { label: "Inativo", color: "bg-muted text-muted-foreground" },
    [StudentStatusEnum.Pending]: { label: "Pendente", color: "bg-destructive/10 text-destructive" },
  }

  const weekLabels: Record<number, string> = {
    [1]: "Semana 1",
    [2]: "Semana 2",
    [3]: "Semana 3",
    [4]: "Semana 4",
  }

  const getWeek = (weekNumber: number | undefined) => {
    if (!weekNumber) return "Desconhecida"
    return [weekLabels[weekNumber]]
  }

  const dayLabels: Record<DayOfWeekEnum, string> = {
    [DayOfWeekEnum.Segunda]: "Segunda",
    [DayOfWeekEnum.Terca]: "Terça",
    [DayOfWeekEnum.Quarta]: "Quarta",
    [DayOfWeekEnum.Quinta]: "Quinta",
    [DayOfWeekEnum.Sexta]: "Sexta",
    [DayOfWeekEnum.Sabado]: "Sábado",
    [DayOfWeekEnum.Domingo]: "Domingo",
  }

  const getDayOfWeek = (day: DayOfWeekEnum | undefined) => {
    if (!day) return ["Desconhecido"]
    return [dayLabels[day]]
  }

  const calculateAge = (birthDate?: Date) => {
    if (!birthDate) return null
    const today = new Date()
    const birth = new Date(birthDate)
    let age = today.getFullYear() - birth.getFullYear()
    const monthDiff = today.getMonth() - birth.getMonth()
    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) {
      age--
    }
    return age
  }

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
              <Link href="/students">
                <ArrowLeft className="mr-2 h-4 w-4" />
                Voltar para alunos
              </Link>
            </Button>
            <div className="rounded-lg border border-destructive/50 bg-destructive/10 p-4 text-destructive">
              {error || "Aluno não encontrado"}
            </div>
          </div>
        </div>
      </div>
    )
  }

  return (
    <AuthGuard requiredRoles={["Trainer"]}>
      <Loading isLoading={!student || loading}></Loading>
      <div className="min-h-screen bg-background p-4 md:p-8">
        <div className="max-w-6xl mx-auto space-y-6">
          <div className="mb-6">
            <Button variant="ghost" asChild className="mb-4">
              <Link href="/students">
                <ArrowLeft className="mr-2 h-4 w-4" />
                Voltar para alunos
              </Link>
            </Button>
          </div>

          {/* Student Header */}
          <Card className="mb-6">
            <CardContent className="p-6">
              <div className="flex flex-col gap-6 md:flex-row md:items-start md:justify-between">
                <div className="flex items-start gap-4">
                  <Avatar className="h-20 w-20">
                    <AvatarImage src={student?.userExtension?.user?.avatarUrl || "/placeholder.svg"} alt={`${student?.userExtension?.user?.firstName}`} />
                    <AvatarFallback>
                      {`${student?.userExtension?.user?.firstName}`
                        .split(" ")
                        .map((n) => n[0])
                        .join("")}
                    </AvatarFallback>
                  </Avatar>

                  <div className="space-y-2">
                    <div className="flex items-center gap-3">
                      <h1 className="text-2xl font-bold">{`${student?.userExtension?.user?.firstName}`}</h1>
                      <Badge className={statusConfig[student?.status ?? 100].color}>{statusConfig[student?.status ?? 100].label}</Badge>
                    </div>

                    <div className="space-y-1 text-sm text-muted-foreground">
                      {student?.userExtension?.user?.email && (
                        <p className="flex items-center gap-2">
                          <Mail className="h-4 w-4" />
                          {student?.userExtension?.user?.email}
                        </p>
                      )}
                      {student?.userExtension?.user?.phoneNumber && (
                        <p className="flex items-center gap-2">
                          <Phone className="h-4 w-4" />
                          {student?.userExtension?.user?.phoneNumber}
                        </p>
                      )}
                    </div>
                  </div>
                </div>

                <div className="flex gap-2">
                  <Button variant="outline" asChild>
                    <Link href={`/messages?student=${student?.userExtensionId}`}>
                      <MessageSquare className="mr-2 h-4 w-4" />
                      Mensagem
                    </Link>
                  </Button>
                  <Button asChild>
                    <Link href={`/students/${student?.userExtensionId}/edit`}>
                      <Edit className="mr-2 h-4 w-4" />
                      Editar
                    </Link>
                  </Button>
                </div>
              </div>
            </CardContent>
          </Card>

          {/* Quick Stats */}
          <div className="grid gap-6 md:grid-cols-4 mb-6">
            <Card>
              <CardContent className="p-6">
                <div className="flex items-center gap-3">
                  <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-primary/10">
                    <TrendingUp className="h-5 w-5 text-primary" />
                  </div>
                  <div>
                    <p className="text-2xl font-bold">{student?.weight ? `${student?.weight}kg` : "-"}</p>
                    <p className="text-xs text-muted-foreground">Peso Atual</p>
                  </div>
                </div>
              </CardContent>
            </Card>

            <Card>
              <CardContent className="p-6">
                <div className="flex items-center gap-3">
                  <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-accent/10">
                    <Dumbbell className="h-5 w-5 text-accent" />
                  </div>
                  <div>
                    <p className="text-2xl font-bold">{student?.workoutHistories?.length || 0}</p>
                    <p className="text-xs text-muted-foreground">Treinos</p>
                  </div>
                </div>
              </CardContent>
            </Card>

            <Card>
              <CardContent className="p-6">
                <div className="flex items-center gap-3">
                  <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-chart-2/10">
                    <Calendar className="h-5 w-5 text-chart-2" />
                  </div>
                  <div>
                    <p className="text-sm font-bold">
                      {student?.workoutHistories?.[0]?.updatedAt
                        ? formatDate(student?.workoutHistories[0].updatedAt)
                        : "-"}
                    </p>
                    <p className="text-xs text-muted-foreground">Último Treino</p>
                  </div>
                </div>
              </CardContent>
            </Card>

            <Card>
              <CardContent className="p-6">
                <div className="flex items-center gap-3">
                  <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-chart-3/10">
                    <FileText className="h-5 w-5 text-chart-3" />
                  </div>
                  <div>
                    <p className="text-sm font-bold">{formatDate(student?.createdAt)}</p>
                    <p className="text-xs text-muted-foreground">Data Início</p>
                  </div>
                </div>
              </CardContent>
            </Card>
          </div>

          {/* Detailed Info */}
          <Tabs defaultValue="overview" className="space-y-6">
            <TabsList>
              <TabsTrigger value="overview">Visão Geral</TabsTrigger>
              <TabsTrigger value="workouts">Histórico de Treinos</TabsTrigger>
              <TabsTrigger value="measurements">Medições</TabsTrigger>
            </TabsList>

            <TabsContent value="overview" className="space-y-6">
              <div className="grid gap-6 lg:grid-cols-2">
                <Card>
                  <CardHeader>
                    <CardTitle>Informações Básicas</CardTitle>
                  </CardHeader>
                  <CardContent className="space-y-4">
                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <p className="text-sm text-muted-foreground">Idade</p>
                        <p className="font-medium">
                          {calculateAge(student?.userExtension?.user?.birthDate) ? `${calculateAge(student?.userExtension?.user?.birthDate)} anos` : "-"}
                        </p>
                      </div>
                      <div>
                        <p className="text-sm text-muted-foreground">Altura</p>
                        <p className="font-medium">{student?.height ? `${student?.height} cm` : "-"}</p>
                      </div>
                      <div>
                        <p className="text-sm text-muted-foreground">Peso</p>
                        <p className="font-medium">{student?.weight ? `${student?.weight} kg` : "-"}</p>
                      </div>
                      <div>
                        <p className="text-sm text-muted-foreground">Objetivo</p>
                        <p className="font-medium">{student?.goal || "-"}</p>
                      </div>
                    </div>
                  </CardContent>
                </Card>

                <Card>
                  <CardHeader>
                    <CardTitle>Progresso do Objetivo</CardTitle>
                  </CardHeader>
                  <CardContent>
                    <div className="space-y-2">
                      <div className="flex justify-between text-sm">
                        <span className="text-muted-foreground">Meta de {student?.goal || "Treino"}</span>
                        <span className="font-semibold">{student?.workoutHistories?.length || 0} treinos</span>
                      </div>
                      <Progress value={Math.min((student?.workoutHistories?.length || 0) * 10, 100)} />
                      <p className="text-xs text-muted-foreground">Continue assim!</p>
                    </div>
                  </CardContent>
                </Card>
              </div>

              {student?.notes && (
                <Card>
                  <CardHeader>
                    <CardTitle>Observações</CardTitle>
                  </CardHeader>
                  <CardContent>
                    <p className="text-sm leading-relaxed">{student?.notes}</p>
                  </CardContent>
                </Card>
              )}
            </TabsContent>

            <TabsContent value="workouts">
              <Card>
                <CardHeader>
                  <CardTitle>Histórico de Treinos</CardTitle>
                </CardHeader>
                <CardContent>
                  {student?.workoutHistories && student?.workoutHistories.length > 0 ? (
                    <div className="space-y-4">
                      {student?.workoutHistories.map((workout) => (
                        <div key={workout.id} className="flex items-center justify-between rounded-lg border p-4">
                          <div className="space-y-1">
                            <p className="font-medium">{getWeek(workout.workoutPlanWeek?.weekNumber)} · {getDayOfWeek(workout.workoutPlanDay?.dayOfWeek)}</p>
                            <p className="text-sm text-muted-foreground">{formatDate(workout.createdAt)}</p>
                          </div>
                          <div className="text-right">
                            <p className="font-semibold">{workout.status || 0}%</p>
                            <p className="text-xs text-muted-foreground">Conclusão</p>
                          </div>
                        </div>
                      ))}
                    </div>
                  ) : (
                    <p className="text-muted-foreground text-center py-8">Nenhum treino registrado</p>
                  )}
                </CardContent>
              </Card>
            </TabsContent>

            <TabsContent value="measurements">
              <Card>
                <CardHeader>
                  <CardTitle>Histórico de Medições</CardTitle>
                </CardHeader>
                <CardContent>
                  {student?.measurements && student?.measurements.length > 0 ? (
                    <div className="space-y-4">
                      {student?.measurements.map((measure) => (
                        <div key={measure.id} className="rounded-lg border p-4">
                          <p className="font-medium mb-3">{formatDate(measure.date)}</p>
                          <div className="grid grid-cols-3 gap-4">
                            <div>
                              <p className="text-sm text-muted-foreground">Peso</p>
                              <p className="text-lg font-semibold">{measure.weight ? `${measure.weight}kg` : "-"}</p>
                            </div>
                            <div>
                              <p className="text-sm text-muted-foreground">% Gordura</p>
                              <p className="text-lg font-semibold">{measure.bodyFat ? `${measure.bodyFat}%` : "-"}</p>
                            </div>
                            <div>
                              <p className="text-sm text-muted-foreground">Massa Muscular</p>
                              <p className="text-lg font-semibold">
                                {measure.muscleMass ? `${measure.muscleMass}kg` : "-"}
                              </p>
                            </div>
                          </div>
                        </div>
                      ))}
                    </div>
                  ) : (
                    <p className="text-muted-foreground text-center py-8">Nenhuma medição registrada</p>
                  )}
                </CardContent>
              </Card>
            </TabsContent>
          </Tabs>
        </div>
      </div>
    </AuthGuard>
  )
}

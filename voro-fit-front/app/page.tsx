import { AppHeader } from "@/components/app-header"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Progress } from "@/components/ui/progress"
import { Users, Dumbbell, TrendingUp, Calendar, AlertCircle, ArrowRight } from "lucide-react"
import { AuthGuard } from "@/components/auth/auth.guard"

// Mock data
const stats = [
  {
    title: "Total de Alunos",
    value: "24",
    change: "+3 este mês",
    icon: Users,
    color: "text-primary",
  },
  {
    title: "Treinos Ativos",
    value: "18",
    change: "5 pendentes",
    icon: Dumbbell,
    color: "text-accent",
  },
  {
    title: "Taxa de Adesão",
    value: "87%",
    change: "+5% vs. mês anterior",
    icon: TrendingUp,
    color: "text-chart-2",
  },
]

const upcomingWorkouts = [
  {
    student: "Carlos Silva",
    time: "09:00",
    type: "Treino A - Peito e Tríceps",
    avatar: "/student-male-studying.png",
  },
  {
    student: "Ana Costa",
    time: "10:30",
    type: "Treino B - Costas e Bíceps",
    avatar: "/diverse-female-student.png",
  },
  {
    student: "Pedro Santos",
    time: "14:00",
    type: "Avaliação Física",
    avatar: "/student-male-fitness.jpg",
  },
]

const recentAlerts = [
  {
    message: "Maria Oliveira não treina há 5 dias",
    time: "2h atrás",
    type: "warning",
  },
  {
    message: "João Souza completou treino semanal",
    time: "4h atrás",
    type: "success",
  },
  {
    message: "Nova mensagem de Lucas Ferreira",
    time: "6h atrás",
    type: "info",
  },
]

const studentProgress = [
  {
    name: "Carlos Silva",
    progress: 85,
    goal: "Hipertrofia",
    avatar: "/student-male-studying.png",
  },
  {
    name: "Ana Costa",
    progress: 70,
    goal: "Emagrecimento",
    avatar: "/diverse-female-student.png",
  },
  {
    name: "Pedro Santos",
    progress: 92,
    goal: "Força",
    avatar: "/student-male-fitness.jpg",
  },
  {
    name: "Mariana Lima",
    progress: 65,
    goal: "Condicionamento",
    avatar: "/student-female-athlete.jpg",
  },
]

export default function DashboardPage() {
  return (
    <AuthGuard requiredRoles={["Admin"]}>
      <div className="flex-1 overflow-y-auto p-6">
        <div className="mb-6">
          <h1 className="text-3xl font-bold text-balance">Dashboard</h1>
          <p className="text-muted-foreground">Bem-vindo de volta! Aqui está um resumo da sua semana.</p>
        </div>

        {/* Stats Grid */}
        <div className="grid gap-6 md:grid-cols-3 mb-6">
          {stats.map((stat) => {
            const Icon = stat.icon
            return (
              <Card key={stat.title}>
                <CardHeader className="flex flex-row items-center justify-between pb-2">
                  <CardTitle className="text-sm font-medium text-muted-foreground">{stat.title}</CardTitle>
                  <Icon className={`h-5 w-5 ${stat.color}`} />
                </CardHeader>
                <CardContent>
                  <div className="text-3xl font-bold">{stat.value}</div>
                  <p className="text-xs text-muted-foreground mt-1">{stat.change}</p>
                </CardContent>
              </Card>
            )
          })}
        </div>

        {/* Main Content Grid */}
        <div className="grid gap-6 lg:grid-cols-2">
          {/* Upcoming Workouts */}
          <Card>
            <CardHeader className="flex flex-row items-center justify-between">
              <CardTitle className="flex items-center gap-2">
                <Calendar className="h-5 w-5" />
                Próximos Treinos
              </CardTitle>
              <Button variant="ghost" size="sm">
                Ver todos
                <ArrowRight className="ml-2 h-4 w-4" />
              </Button>
            </CardHeader>
            <CardContent>
              <div className="space-y-4">
                {upcomingWorkouts.map((workout, i) => (
                  <div key={i} className="flex items-center gap-4 rounded-lg border p-4">
                    <Avatar>
                      <AvatarImage src={workout.avatar || "/placeholder.svg"} alt={workout.student} />
                      <AvatarFallback>
                        {workout.student
                          .split(" ")
                          .map((n) => n[0])
                          .join("")}
                      </AvatarFallback>
                    </Avatar>
                    <div className="flex-1">
                      <p className="font-medium">{workout.student}</p>
                      <p className="text-sm text-muted-foreground">{workout.type}</p>
                    </div>
                    <div className="text-right">
                      <p className="font-semibold">{workout.time}</p>
                      <p className="text-xs text-muted-foreground">Hoje</p>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>

          {/* Recent Alerts */}
          <Card>
            <CardHeader className="flex flex-row items-center justify-between">
              <CardTitle className="flex items-center gap-2">
                <AlertCircle className="h-5 w-5" />
                Alertas Recentes
              </CardTitle>
              <Button variant="ghost" size="sm">
                Ver todos
                <ArrowRight className="ml-2 h-4 w-4" />
              </Button>
            </CardHeader>
            <CardContent>
              <div className="space-y-4">
                {recentAlerts.map((alert, i) => (
                  <div key={i} className="flex items-start gap-3 rounded-lg border p-4">
                    <div
                      className={`mt-0.5 h-2 w-2 rounded-full ${
                        alert.type === "warning"
                          ? "bg-destructive"
                          : alert.type === "success"
                            ? "bg-accent"
                            : "bg-primary"
                      }`}
                    />
                    <div className="flex-1">
                      <p className="text-sm">{alert.message}</p>
                      <p className="text-xs text-muted-foreground mt-1">{alert.time}</p>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>

          {/* Student Progress */}
          <Card className="lg:col-span-2">
            <CardHeader>
              <CardTitle>Progresso dos Alunos</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-6">
                {studentProgress.map((student) => (
                  <div key={student.name} className="space-y-2">
                    <div className="flex items-center justify-between">
                      <div className="flex items-center gap-3">
                        <Avatar className="h-8 w-8">
                          <AvatarImage src={student.avatar || "/placeholder.svg"} alt={student.name} />
                          <AvatarFallback>
                            {student.name
                              .split(" ")
                              .map((n) => n[0])
                              .join("")}
                          </AvatarFallback>
                        </Avatar>
                        <div>
                          <p className="font-medium text-sm">{student.name}</p>
                          <p className="text-xs text-muted-foreground">{student.goal}</p>
                        </div>
                      </div>
                      <span className="text-sm font-semibold">{student.progress}%</span>
                    </div>
                    <Progress value={student.progress} />
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </AuthGuard>
  )
}

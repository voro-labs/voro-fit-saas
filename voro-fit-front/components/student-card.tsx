import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { Card, CardContent } from "@/components/ui/card"
import { Eye } from "lucide-react"
import Link from "next/link"

interface StudentCardProps {
  id: string
  name: string
  age?: number | null
  height?: number | null
  weight?: number | null
  status: "active" | "inactive" | "pending"
  avatar?: string
  goal?: string
}

export function StudentCard({ id, name, age, height, weight, status, avatar, goal }: StudentCardProps) {
  const statusConfig = {
    active: { label: "Ativo", color: "bg-accent text-accent-foreground" },
    inactive: { label: "Inativo", color: "bg-muted text-muted-foreground" },
    pending: { label: "Pendente", color: "bg-destructive/10 text-destructive" },
  }

  return (
    <Card className="overflow-hidden hover:shadow-md transition-shadow">
      <CardContent className="p-4">
        <div className="flex items-center gap-4">
          <Avatar className="h-14 w-14">
            <AvatarImage src={avatar || "/placeholder.svg"} alt={name} />
            <AvatarFallback>
              {name
                .split(" ")
                .map((n) => n[0])
                .join("")}
            </AvatarFallback>
          </Avatar>

          <div className="flex-1 min-w-0">
            <h3 className="font-semibold truncate">{name}</h3>
            <div className="flex items-center gap-3 text-sm text-muted-foreground mt-1">
              {age && <span>{age} anos</span>}
              {age && weight && <span>•</span>}
              {weight && <span>{weight}kg</span>}
              {goal && (
                <>
                  {(age || weight) && <span>•</span>}
                  <span className="truncate">{goal}</span>
                </>
              )}
              {!age && !weight && !goal && <span>Sem informações adicionais</span>}
            </div>
          </div>

          <div className="flex items-center gap-2">
            <Badge className={statusConfig[status].color}>{statusConfig[status].label}</Badge>
            <Button asChild size="sm" variant="outline">
              <Link href={`/students/${id}`}>
                <Eye className="h-4 w-4 mr-2" />
                Ver
              </Link>
            </Button>
          </div>
        </div>
      </CardContent>
    </Card>
  )
}

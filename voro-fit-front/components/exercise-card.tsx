import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardFooter } from "@/components/ui/card"
import { Eye, Globe, User } from "lucide-react"
import Link from "next/link"

interface ExerciseCardProps {
  id: string
  name: string
  muscleGroup: string
  type: "public" | "custom"
  thumbnail?: string
}

export function ExerciseCard({ id, name, muscleGroup, type, thumbnail }: ExerciseCardProps) {
  return (
    <Card className="overflow-hidden hover:shadow-md transition-shadow">
      <div className="aspect-video bg-muted relative overflow-hidden">
        {thumbnail ? (
          <img src={thumbnail || "/placeholder.svg"} alt={name} className="w-full h-full object-cover" />
        ) : (
          <div className="flex items-center justify-center w-full h-full">
            <div className="text-muted-foreground">
              <svg className="h-16 w-16" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={1}
                  d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z"
                />
              </svg>
            </div>
          </div>
        )}
        <div className="absolute top-2 right-2">
          <Badge variant="secondary" className="flex items-center gap-1">
            {type === "public" ? (
              <>
                <Globe className="h-3 w-3" />
                Público
              </>
            ) : (
              <>
                <User className="h-3 w-3" />
                Personalizado
              </>
            )}
          </Badge>
        </div>
      </div>

      <CardContent className="p-4">
        <h3 className="font-semibold text-balance line-clamp-1">{name}</h3>
        <p className="text-sm text-muted-foreground mt-1">{muscleGroup}</p>
      </CardContent>

      <CardFooter className="p-4 pt-0">
        <Button asChild variant="outline" size="sm" className="w-full bg-transparent">
          <Link href={`/exercises/${id}`}>
            <Eye className="h-4 w-4 mr-2" />
            Ver Detalhes
          </Link>
        </Button>
      </CardFooter>
    </Card>
  )
}

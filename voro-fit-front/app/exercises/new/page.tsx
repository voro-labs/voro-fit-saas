"use client"

import type React from "react"
import { useState } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { ArrowLeft, Video, Loader2 } from "lucide-react"
import Link from "next/link"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { useExercises } from "@/hooks/use-exercises.hook"
import { AuthGuard } from "@/components/auth/auth.guard"

export default function NewExercisePage() {
  const router = useRouter()
  const { createExercise, loading, error } = useExercises()
  const [mediaPreview, setMediaPreview] = useState<string>("")
  const [mediaFile, setMediaFile] = useState<File | null>(null)
  const [muscleGroup, setMuscleGroup] = useState("")

  const handleMediaChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0]
    if (file) {
      setMediaFile(file)
      const reader = new FileReader()
      reader.onloadend = () => {
        setMediaPreview(reader.result as string)
      }
      reader.readAsDataURL(file)
    }
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const formData = new FormData(e.currentTarget)

    formData.set("muscleGroup", muscleGroup)

    if (mediaFile) {
      formData.append("media", mediaFile)
    }

    const result = await createExercise(formData)
    if (result) {
      router.push("/exercises")
    }
  }

  return (
    <AuthGuard requiredRoles={["Trainer"]}>
      <div className="min-h-screen bg-background p-4 md:p-8">
        <div className="max-w-6xl mx-auto space-y-6">
          <div className="mb-6">
            <Button variant="ghost" asChild className="mb-4">
              <Link href="/exercises">
                <ArrowLeft className="mr-2 h-4 w-4" />
                Voltar para biblioteca
              </Link>
            </Button>

            <h1 className="text-3xl font-bold text-balance">Novo Exercício</h1>
            <p className="text-muted-foreground">Cadastre um novo exercício personalizado</p>
          </div>

          {error && (
            <div className="mb-6 rounded-lg border border-destructive/50 bg-destructive/10 p-4 text-destructive max-w-2xl">
              {error}
            </div>
          )}

          <Card className="max-w-2xl">
            <CardHeader>
              <CardTitle>Informações do Exercício</CardTitle>
              <CardDescription>Preencha os detalhes do exercício</CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleSubmit} className="space-y-6">
                {/* Name */}
                <div className="space-y-2">
                  <Label htmlFor="name">Nome do Exercício *</Label>
                  <Input id="name" name="name" placeholder="Ex: Supino Inclinado com Halteres" required />
                </div>

                {/* Muscle Group */}
                <div className="space-y-2">
                  <Label htmlFor="muscle">Grupo Muscular *</Label>
                  <Select value={muscleGroup} onValueChange={setMuscleGroup} required>
                    <SelectTrigger>
                      <SelectValue placeholder="Selecione o grupo muscular" />
                    </SelectTrigger>
                    <SelectContent>
                      <SelectItem value="Peito">Peito</SelectItem>
                      <SelectItem value="Costas">Costas</SelectItem>
                      <SelectItem value="Pernas">Pernas</SelectItem>
                      <SelectItem value="Ombros">Ombros</SelectItem>
                      <SelectItem value="Bíceps">Bíceps</SelectItem>
                      <SelectItem value="Tríceps">Tríceps</SelectItem>
                      <SelectItem value="Abdômen">Abdômen</SelectItem>
                      <SelectItem value="Glúteos">Glúteos</SelectItem>
                      <SelectItem value="Panturrilha">Panturrilha</SelectItem>
                    </SelectContent>
                  </Select>
                </div>

                {/* Description */}
                <div className="space-y-2">
                  <Label htmlFor="description">Descrição *</Label>
                  <Textarea
                    id="description"
                    name="description"
                    placeholder="Descreva como executar o exercício..."
                    rows={4}
                    required
                  />
                </div>

                {/* Media Upload */}
                <div className="space-y-2">
                  <Label htmlFor="media">Vídeo ou Imagem</Label>
                  {mediaPreview && (
                    <div className="rounded-lg border overflow-hidden mb-2">
                      {mediaPreview.startsWith("data:image") ? (
                        <img
                          src={mediaPreview || "/placeholder.svg"}
                          alt="Preview"
                          className="w-full aspect-video object-cover"
                        />
                      ) : (
                        <video src={mediaPreview} controls className="w-full aspect-video" />
                      )}
                    </div>
                  )}
                  <Label htmlFor="media" className="cursor-pointer">
                    <div className="flex items-center gap-3 rounded-lg border-2 border-dashed border-border p-6 hover:border-primary transition-colors">
                      <div className="flex h-12 w-12 items-center justify-center rounded-lg bg-muted">
                        <Video className="h-6 w-6 text-muted-foreground" />
                      </div>
                      <div>
                        <p className="text-sm font-medium">Clique para fazer upload de vídeo ou imagem</p>
                        <p className="text-xs text-muted-foreground">MP4, MOV, JPG, PNG até 50MB</p>
                      </div>
                    </div>
                  </Label>
                  <Input
                    id="media"
                    type="file"
                    accept="video/*,image/*"
                    className="sr-only"
                    onChange={handleMediaChange}
                  />
                </div>

                {/* Technical Notes */}
                <div className="space-y-2">
                  <Label htmlFor="notes">Observações Técnicas</Label>
                  <Textarea id="notes" name="notes" placeholder="Dicas de execução, cuidados, postura..." rows={3} />
                </div>

                {/* Alternatives */}
                <div className="space-y-2">
                  <Label htmlFor="alternatives">Alternativas de Execução</Label>
                  <Textarea
                    id="alternatives"
                    name="alternatives"
                    placeholder="Variações do exercício, alternativas com outros equipamentos..."
                    rows={3}
                  />
                </div>

                {/* Actions */}
                <div className="flex gap-3 justify-end pt-4">
                  <Button type="button" variant="outline" asChild>
                    <Link href="/exercises">Cancelar</Link>
                  </Button>
                  <Button type="submit" disabled={loading || !muscleGroup}>
                    {loading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                    Cadastrar Exercício
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

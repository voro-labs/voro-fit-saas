"use client"

import type React from "react"
import { useState, useEffect } from "react"
import { useParams, useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { ArrowLeft, Video, Loader2, Dumbbell, FileText, Lightbulb, Save } from "lucide-react"
import Link from "next/link"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { useExercises } from "@/hooks/use-exercises.hook"
import { AuthGuard } from "@/components/auth/auth.guard"
import { Loading } from "@/components/ui/custom/loading/loading"

export default function EditExercisePage() {
  const params = useParams()
  const router = useRouter()
  const { fetchExerciseById, updateExercise, loading, error } = useExercises()
  const [mediaPreview, setMediaPreview] = useState<string>("")
  const [mediaFile, setMediaFile] = useState<File | null>(null)
  const [muscleGroup, setMuscleGroup] = useState("")
  const [formData, setFormData] = useState({
    name: "",
    description: "",
    notes: "",
    alternatives: "",
  })
  const [loadingData, setLoadingData] = useState(true)

  useEffect(() => {
    if (params.id) {
      fetchExerciseById(params.id as string).then((data) => {
        if (data) {
          setFormData({
            name: data.name || "",
            description: data.description || "",
            notes: data.notes || "",
            alternatives: data.alternatives || "",
          })
          setMuscleGroup(data.muscleGroup || "")
          if (data.thumbnailUrl) {
            setMediaPreview(data.thumbnailUrl)
          } else if (data.videoUrl) {
            setMediaPreview(data.videoUrl)
          }
        }
        setLoadingData(false)
      })
    }
  }, [params.id, fetchExerciseById])

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
    const formDataToSend = new FormData(e.currentTarget)

    formDataToSend.set("muscleGroup", muscleGroup)

    if (mediaFile) {
      formDataToSend.append("media", mediaFile)
    }

    const result = await updateExercise(params.id as string, formDataToSend)
    if (result) {
      router.push(`/exercises/${params.id}`)
    }
  }

  if (loadingData) {
    return <Loading isLoading={true} />
  }

  return (
    <AuthGuard requiredRoles={["Trainer"]}>
      <div className="min-h-screen bg-linear-to-br from-background via-background to-muted/20">
        <div className="max-w-4xl mx-auto p-4 md:p-8 space-y-6">
          <div className="space-y-4">
            <Button variant="ghost" size="sm" asChild className="group">
              <Link href={`/exercises/${params.id}`}>
                <ArrowLeft className="mr-2 h-4 w-4 transition-transform group-hover:-translate-x-1" />
                Voltar para detalhes
              </Link>
            </Button>

            <div className="flex items-center gap-4">
              <div className="flex h-14 w-14 items-center justify-center rounded-xl bg-primary/10">
                <Dumbbell className="h-7 w-7 text-primary" />
              </div>
              <div>
                <h1 className="text-3xl font-bold text-balance">Editar Exercício</h1>
                <p className="text-muted-foreground">Atualize as informações do exercício</p>
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
              <CardTitle className="text-2xl">Informações do Exercício</CardTitle>
              <CardDescription>Edite os detalhes do exercício</CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleSubmit} className="space-y-8">
                {/* Basic Information Section */}
                <div className="space-y-6">
                  <div className="flex items-center gap-2 text-sm font-semibold text-primary">
                    <FileText className="h-4 w-4" />
                    <span>Informações Básicas</span>
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="name" className="text-base">
                      Nome do Exercício *
                    </Label>
                    <Input
                      id="name"
                      name="name"
                      placeholder="Ex: Supino Inclinado com Halteres"
                      required
                      className="h-12 text-base"
                      value={formData.name}
                      onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                    />
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="muscle" className="text-base">
                      Grupo Muscular *
                    </Label>
                    <Select value={muscleGroup} onValueChange={setMuscleGroup} required>
                      <SelectTrigger className="h-12 text-base">
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

                  <div className="space-y-2">
                    <Label htmlFor="description" className="text-base">
                      Descrição *
                    </Label>
                    <Textarea
                      id="description"
                      name="description"
                      placeholder="Descreva como executar o exercício..."
                      rows={4}
                      required
                      className="resize-none text-base"
                      value={formData.description}
                      onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                    />
                  </div>
                </div>

                {/* Media Upload Section */}
                <div className="space-y-4">
                  <div className="flex items-center gap-2 text-sm font-semibold text-primary">
                    <Video className="h-4 w-4" />
                    <span>Mídia de Demonstração</span>
                  </div>

                  {mediaPreview && (
                    <div className="rounded-xl border-2 border-border overflow-hidden shadow-md animate-in fade-in zoom-in-95">
                      {mediaPreview.startsWith("data:image") ||
                      mediaPreview.endsWith(".jpg") ||
                      mediaPreview.endsWith(".png") ? (
                        <img
                          src={mediaPreview || "/placeholder.svg"}
                          alt="Preview"
                          className="w-full aspect-video object-cover"
                        />
                      ) : (
                        <video src={mediaPreview} controls className="w-full aspect-video bg-muted" />
                      )}
                    </div>
                  )}

                  <Label htmlFor="media" className="cursor-pointer block">
                    <div className="flex items-center gap-4 rounded-xl border-2 border-dashed border-border p-6 hover:border-primary hover:bg-primary/5 transition-all duration-200">
                      <div className="flex h-14 w-14 items-center justify-center rounded-xl bg-primary/10">
                        <Video className="h-7 w-7 text-primary" />
                      </div>
                      <div className="flex-1">
                        <p className="font-medium">Clique para alterar vídeo ou imagem</p>
                        <p className="text-sm text-muted-foreground mt-0.5">MP4, MOV, JPG, PNG até 50MB</p>
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

                {/* Additional Information Section */}
                <div className="space-y-6">
                  <div className="flex items-center gap-2 text-sm font-semibold text-primary">
                    <Lightbulb className="h-4 w-4" />
                    <span>Informações Adicionais</span>
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="notes" className="text-base">
                      Observações Técnicas
                    </Label>
                    <Textarea
                      id="notes"
                      name="notes"
                      placeholder="Dicas de execução, cuidados, postura..."
                      rows={3}
                      className="resize-none text-base"
                      value={formData.notes}
                      onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
                    />
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="alternatives" className="text-base">
                      Alternativas de Execução
                    </Label>
                    <Textarea
                      id="alternatives"
                      name="alternatives"
                      placeholder="Variações do exercício, alternativas com outros equipamentos..."
                      rows={3}
                      className="resize-none text-base"
                      value={formData.alternatives}
                      onChange={(e) => setFormData({ ...formData, alternatives: e.target.value })}
                    />
                  </div>
                </div>

                <div className="flex gap-3 justify-end pt-6 border-t">
                  <Button type="button" variant="outline" size="lg" asChild>
                    <Link href={`/exercises/${params.id}`}>Cancelar</Link>
                  </Button>
                  <Button type="submit" size="lg" disabled={loading || !muscleGroup} className="min-w-[180px]">
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

"use client"

import type React from "react"
import { useState } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { ArrowLeft, Upload, User, Loader2 } from "lucide-react"
import Link from "next/link"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { useStudents } from "@/hooks/use-students.hook"
import { AuthGuard } from "@/components/auth/auth.guard"

export default function NewStudentPage() {
  const router = useRouter()
  const { createStudent, loading, error } = useStudents()
  const [avatarPreview, setAvatarPreview] = useState<string>("")
  const [avatarFile, setAvatarFile] = useState<File | null>(null)

  const handleAvatarChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0]
    if (file) {
      setAvatarFile(file)
      const reader = new FileReader()
      reader.onloadend = () => {
        setAvatarPreview(reader.result as string)
      }
      reader.readAsDataURL(file)
    }
  }

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const formData = new FormData(e.currentTarget)

    if (avatarFile) {
      formData.append("avatar", avatarFile)
    }

    const result = await createStudent(formData)
    if (result) {
      router.push("/students")
    }
  }

  return (
    <AuthGuard requiredRoles={["Trainer"]}>
      <div className="min-h-screen bg-background p-4 md:p-8">
        <div className="max-w-6xl mx-auto space-y-6">
          <div className="mb-6">
            <Button variant="ghost" asChild className="mb-4">
              <Link href="/students">
                <ArrowLeft className="mr-2 h-4 w-4" />
                Voltar para alunos
              </Link>
            </Button>

            <h1 className="text-3xl font-bold text-balance">Novo Aluno</h1>
            <p className="text-muted-foreground">Preencha as informações do novo aluno</p>
          </div>

          {error && (
            <div className="mb-6 rounded-lg border border-destructive/50 bg-destructive/10 p-4 text-destructive max-w-2xl">
              {error}
            </div>
          )}

          <Card className="max-w-2xl">
            <CardHeader>
              <CardTitle>Informações Pessoais</CardTitle>
              <CardDescription>Cadastre os dados básicos do aluno</CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleSubmit} className="space-y-6">
                {/* Avatar Upload */}
                <div className="flex items-center gap-6">
                  <Avatar className="h-24 w-24">
                    <AvatarImage src={avatarPreview || "/placeholder.svg"} alt="Avatar" />
                    <AvatarFallback>
                      <User className="h-12 w-12 text-muted-foreground" />
                    </AvatarFallback>
                  </Avatar>
                  <div className="flex-1">
                    <Label htmlFor="avatar" className="cursor-pointer">
                      <div className="flex items-center gap-2 rounded-lg border-2 border-dashed border-border p-4 hover:border-primary transition-colors">
                        <Upload className="h-5 w-5 text-muted-foreground" />
                        <div>
                          <p className="text-sm font-medium">Clique para fazer upload</p>
                          <p className="text-xs text-muted-foreground">PNG, JPG até 5MB</p>
                        </div>
                      </div>
                    </Label>
                    <Input id="avatar" type="file" accept="image/*" className="sr-only" onChange={handleAvatarChange} />
                  </div>
                </div>

                {/* Name */}
                <div className="space-y-2">
                  <Label htmlFor="name">Nome Completo *</Label>
                  <Input id="name" name="name" placeholder="Ex: Carlos Silva" required />
                </div>

                {/* Email and Phone */}
                <div className="grid gap-4 sm:grid-cols-2">
                  <div className="space-y-2">
                    <Label htmlFor="email">Email</Label>
                    <Input id="email" name="email" type="email" placeholder="email@exemplo.com" />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="phone">Telefone</Label>
                    <Input id="phone" name="phone" placeholder="(11) 99999-9999" />
                  </div>
                </div>

                {/* Birth Date, Height, Weight */}
                <div className="grid gap-4 sm:grid-cols-3">
                  <div className="space-y-2">
                    <Label htmlFor="birthDate">Data de Nascimento</Label>
                    <Input id="birthDate" name="birthDate" type="date" />
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="height">Altura (cm)</Label>
                    <Input id="height" name="height" type="number" placeholder="178" />
                  </div>

                  <div className="space-y-2">
                    <Label htmlFor="weight">Peso (kg)</Label>
                    <Input id="weight" name="weight" type="number" step="0.1" placeholder="82.5" />
                  </div>
                </div>

                {/* Goal */}
                <div className="space-y-2">
                  <Label htmlFor="goal">Objetivo</Label>
                  <Input id="goal" name="goal" placeholder="Ex: Hipertrofia, Emagrecimento, Força" />
                </div>

                {/* Notes */}
                <div className="space-y-2">
                  <Label htmlFor="notes">Observações</Label>
                  <Textarea
                    id="notes"
                    name="notes"
                    placeholder="Informações adicionais sobre o aluno, histórico médico, restrições..."
                    rows={4}
                  />
                </div>

                {/* Actions */}
                <div className="flex gap-3 justify-end pt-4">
                  <Button type="button" variant="outline" asChild>
                    <Link href="/students">Cancelar</Link>
                  </Button>
                  <Button type="submit" disabled={loading}>
                    {loading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                    Cadastrar Aluno
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

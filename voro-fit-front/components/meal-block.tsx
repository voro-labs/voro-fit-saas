"use client"

import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { X, Clock } from "lucide-react"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { TimePicker } from "@/components/ui/custom/time-picker"

interface Meal {
  id: string
  time: string
  period: string
  description: string
  quantity?: string
  notes?: string
}

interface MealBlockProps {
  meal: Meal
  onRemove: () => void
  onChange: (field: keyof Meal, value: string) => void
}

export function MealBlock({ meal, onRemove, onChange }: MealBlockProps) {
  return (
    <Card>
      <CardHeader className="pb-3">
        <div className="flex items-center justify-between">
          <CardTitle className="text-base flex items-center gap-2">
            <Clock className="h-4 w-4" />
            Refeição
          </CardTitle>
          <Button variant="ghost" size="icon" onClick={onRemove}>
            <X className="h-4 w-4" />
          </Button>
        </div>
      </CardHeader>
      <CardContent className="space-y-4">
        <div className="grid gap-4 sm:grid-cols-2">
          <div className="space-y-2">
            <Label>Período</Label>
            <Select value={meal.period} onValueChange={(value) => onChange("period", value)}>
              <SelectTrigger>
                <SelectValue placeholder="Selecione o período" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="Café da Manhã">Café da Manhã</SelectItem>
                <SelectItem value="Lanche da Manhã">Lanche da Manhã</SelectItem>
                <SelectItem value="Almoço">Almoço</SelectItem>
                <SelectItem value="Lanche da Tarde">Lanche da Tarde</SelectItem>
                <SelectItem value="Jantar">Jantar</SelectItem>
                <SelectItem value="Ceia">Ceia</SelectItem>
              </SelectContent>
            </Select>
          </div>

          <div className="space-y-2">
            <Label>Horário</Label>
            <TimePicker
              value={meal.time} 
              onChange={(value) => onChange("time", value)} 
              placeholder="hh:mm"
              className="h-12 text-base"
            />
          </div>
        </div>

        <div className="space-y-2">
          <Label>Descrição dos Alimentos *</Label>
          <Textarea
            placeholder="Ex: 2 ovos mexidos, 2 fatias de pão integral, 1 banana..."
            rows={3}
            value={meal.description}
            onChange={(e) => onChange("description", e.target.value)}
          />
        </div>

        <div className="space-y-2">
          <Label>Quantidade Total (opcional)</Label>
          <Input
            placeholder="Ex: Aproximadamente 500 calorias"
            value={meal.quantity}
            onChange={(e) => onChange("quantity", e.target.value)}
          />
        </div>

        <div className="space-y-2">
          <Label>Observações</Label>
          <Textarea
            placeholder="Dicas de preparo, substituições possíveis..."
            rows={2}
            value={meal.notes}
            onChange={(e) => onChange("notes", e.target.value)}
          />
        </div>
      </CardContent>
    </Card>
  )
}

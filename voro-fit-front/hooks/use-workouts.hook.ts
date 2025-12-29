"use client"

import { useState, useEffect, useCallback } from "react"
import { API_CONFIG, secureApiCall } from "@/lib/api"
import type { WorkoutHistoryDto } from "@/types/DTOs/workout-history.interface"

export function useWorkouts() {
  const [workouts, setWorkouts] = useState<WorkoutHistoryDto[]>([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const fetchWorkouts = useCallback(async () => {
    setLoading(true)
    setError(null)

    try {
      const response = await secureApiCall<WorkoutHistoryDto[]>(API_CONFIG.ENDPOINTS.WORKOUTS, {
        method: "GET",
      })

      if (response.hasError) throw new Error(response.message ?? "Erro ao carregar treinos")

      setWorkouts(response.data ?? [])
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro desconhecido")
    } finally {
      setLoading(false)
    }
  }, [])

  const fetchWorkoutById = useCallback(async (id: string): Promise<WorkoutHistoryDto | null> => {
    setLoading(true)
    setError(null)

    try {
      const response = await secureApiCall<WorkoutHistoryDto>(`${API_CONFIG.ENDPOINTS.WORKOUTS}/${id}`, {
        method: "GET",
      })

      if (response.hasError) throw new Error(response.message ?? "Erro ao carregar treino")

      return response.data ?? null
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro desconhecido")
      return null
    } finally {
      setLoading(false)
    }
  }, [])

  const createWorkout = useCallback(async (data: WorkoutHistoryDto): Promise<WorkoutHistoryDto | null> => {
    setLoading(true)
    setError(null)

    try {
      const response = await secureApiCall<WorkoutHistoryDto>(API_CONFIG.ENDPOINTS.WORKOUTS, {
        method: "POST",
        body: JSON.stringify(data),
      })

      if (response.hasError) throw new Error(response.message ?? "Erro ao criar treino")

      if (response.data) {
        setWorkouts((prev) => [...prev, response.data!])
      }

      return response.data ?? null
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro desconhecido")
      return null
    } finally {
      setLoading(false)
    }
  }, [])

  const updateWorkout = useCallback(
    async (id: string, data: WorkoutHistoryDto): Promise<WorkoutHistoryDto | null> => {
      setLoading(true)
      setError(null)

      try {
        const response = await secureApiCall<WorkoutHistoryDto>(`${API_CONFIG.ENDPOINTS.WORKOUTS}/${id}`, {
          method: "PUT",
          body: JSON.stringify(data),
        })

        if (response.hasError) throw new Error(response.message ?? "Erro ao atualizar treino")

        if (response.data) {
          setWorkouts((prev) => prev.map((w) => (w.id === id ? response.data! : w)))
        }

        return response.data ?? null
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
        return null
      } finally {
        setLoading(false)
      }
    },
    [],
  )

  const deleteWorkout = useCallback(async (id: string): Promise<boolean> => {
    setLoading(true)
    setError(null)

    try {
      const response = await secureApiCall<void>(`${API_CONFIG.ENDPOINTS.WORKOUTS}/${id}`, {
        method: "DELETE",
      })

      if (response.hasError) throw new Error(response.message ?? "Erro ao excluir treino")

      setWorkouts((prev) => prev.filter((w) => w.id !== id))
      return true
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro desconhecido")
      return false
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    fetchWorkouts()
  }, [fetchWorkouts])

  return {
    workouts,
    loading,
    error,
    fetchWorkouts,
    fetchWorkoutById,
    createWorkout,
    updateWorkout,
    deleteWorkout,
    clearError: () => setError(null),
  }
}

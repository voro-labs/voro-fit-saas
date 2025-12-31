"use client"

import { useState, useEffect, useCallback } from "react"
import { API_CONFIG, secureApiCall } from "@/lib/api"
import type { ContactDto } from "@/types/DTOs/contact.interface"
import type { MessageDto } from "@/types/DTOs/message.interface"
import { MessageStatusEnum } from "@/types/Enums/messageStatusEnum.enum"

export function useEvolutionChat(instanceId: string) {
  const [contacts, setContacts] = useState<ContactDto[]>([])
  const [messages, setMessages] = useState<Record<string, MessageDto[]>>({})
  const [selectedContactId, setSelectedContactId] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  // 🔹 Buscar contatos
  const fetchContacts = useCallback(async () => {
    if (!instanceId) return

    setLoading(true)
    setError(null)

    try {
      const response = await secureApiCall<ContactDto[]>(`${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/contacts`, {
        method: "GET",
      })

      if (response.hasError) throw new Error(response.message ?? "Erro ao carregar contatos")

      setContacts(response.data ?? [])
    } catch (err) {
      setError(err instanceof Error ? err.message : "Erro desconhecido")
    } finally {
      setLoading(false)
    }
  }, [instanceId])

  // 🔹 Salvar contato
  const saveContact = useCallback(
    async (displayName: string, number: string) => {
      if (!displayName || !number || !instanceId) return
      setError(null)

      try {
        const body = { DisplayName: displayName, Number: number, InstanceId: instanceId }

        const response = await secureApiCall<ContactDto>(`${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/contacts/save`, {
          method: "POST",
          body: JSON.stringify(body),
        })

        if (response.hasError) throw new Error(response.message ?? "Erro ao salvar contato")

        let newContact: ContactDto = {
          id: Date.now().toString(),
          displayName,
          number: number,
        }

        if (response.data) newContact = response.data

        setContacts((prev) => [...prev, newContact])
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Atualizar contato
  const updateContact = useCallback(
    async (contactId: string, displayName: string, number: string, profilePicture: File | null) => {
      if (!displayName || !number || !instanceId) return
      setError(null)

      try {
        const form = new FormData()

        form.append("displayName", displayName)
        form.append("number", number)
        form.append("instanceId", instanceId)

        if (profilePicture) form.append("profilePicture", profilePicture)

        const response = await secureApiCall<ContactDto>(
          `${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/contacts/${contactId}/update`,
          {
            method: "PUT",
            body: form,
          },
        )

        if (response.hasError) throw new Error(response.message ?? "Erro ao salvar contato")

        let newContact: ContactDto = {
          id: contactId,
          displayName,
          number: number,
        }

        if (response.data) newContact = response.data

        setContacts((prev) => {
          const exists = prev.some((c) => c.id === newContact.id)
          if (exists) {
            return prev.map((c) => (c.id === newContact.id ? newContact : c))
          }
          return [...prev, newContact]
        })
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Buscar mensagens com um contato
  const fetchMessages = useCallback(
    async (contactId: string) => {
      if (!contactId || !instanceId) return
      setError(null)

      try {
        const response = await secureApiCall<MessageDto[]>(
          `${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/messages/${contactId}`,
          {
            method: "GET",
          },
        )

        if (response.hasError) throw new Error(response.message ?? "Erro ao carregar mensagens")

        setMessages((prev) => ({
          ...prev,
          [contactId]: response.data ?? [],
        }))
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Enviar mensagem
  const sendMessage = useCallback(
    async (contactId: string, text: string) => {
      if (!contactId || !text || !instanceId) return
      setError(null)

      try {
        const body = { text: text }

        const response = await secureApiCall<MessageDto>(
          `${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/messages/${contactId}/send`,
          {
            method: "POST",
            body: JSON.stringify(body),
          },
        )

        if (response.hasError) throw new Error(response.message ?? "Erro ao enviar mensagem")

        let newMessage: MessageDto = {
          id: Date.now().toString(),
          content: text,
          sentAt: new Date(),
          status: MessageStatusEnum.Created,
          isFromMe: true,
          contactId: contactId,
          contact: {
            lastMessage: text,
            lastMessageAt: Date.now().toString(),
          } as ContactDto,
          messageReactions: [],
        }

        if (response.data) newMessage = response.data

        setMessages((prev) => ({
          ...prev,
          [contactId]: [...(prev[contactId] || []), newMessage],
        }))
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Enviar anexo
  const sendAttachment = useCallback(
    async (contactId: string, attachment: File) => {
      if (!contactId || !attachment || !instanceId) return
      setError(null)

      try {
        const form = new FormData()

        form.append("attachment", attachment)

        const response = await secureApiCall<MessageDto>(
          `${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/messages/${contactId}/send/attachment`,
          {
            method: "POST",
            body: form,
          },
        )

        if (response.hasError) throw new Error(response.message ?? "Erro ao enviar anexo")

        const url = URL.createObjectURL(attachment)

        let newMessage: MessageDto = {
          id: Date.now().toString(),
          content: "",
          base64: url,
          sentAt: new Date(),
          status: MessageStatusEnum.Created,
          isFromMe: true,
          contactId: contactId,
          contact: {
            lastMessage: "Anexo enviado",
            lastMessageAt: Date.now().toString(),
          } as ContactDto,
          messageReactions: [],
        }

        if (response.data) newMessage = response.data

        setMessages((prev) => ({
          ...prev,
          [contactId]: [...(prev[contactId] || []), newMessage],
        }))
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Deletar mensagem
  const deleteMessage = useCallback(
    async (contactId: string, message: MessageDto) => {
      if (!contactId || !message.id || !instanceId) return
      setError(null)

      try {
        const body = { id: message.id }

        const response = await secureApiCall<MessageDto>(
          `${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/messages/${contactId}/delete`,
          {
            method: "POST",
            body: JSON.stringify(body),
          },
        )

        if (response.hasError) throw new Error(response.message ?? "Erro ao deletar mensagem")

        setMessages((prev) => ({
          ...prev,
          [contactId]: [...(prev[contactId].filter((m) => m.id !== message.id) || [])],
        }))
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Encaminhar mensagem
  const forwardMessage = useCallback(
    async (contactId: string, message: MessageDto | null) => {
      if (!contactId || !message?.id || !instanceId) return
      setError(null)

      try {
        const body = { id: message.id }

        const response = await secureApiCall<MessageDto>(
          `${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/messages/${contactId}/forward`,
          {
            method: "POST",
            body: JSON.stringify(body),
          },
        )

        if (response.hasError) throw new Error(response.message ?? "Erro ao encaminhar mensagem")

        setMessages((prev) => ({
          ...prev,
          [contactId]: [...(prev[contactId] || [])],
        }))
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Enviar mensagem com citação
  const sendQuotedMessage = useCallback(
    async (contactId: string, message: MessageDto, text: string) => {
      if (!contactId || !message.id || !text || !instanceId) return
      setError(null)

      try {
        const body = { text, quoted: { key: { id: message.id } } }

        const response = await secureApiCall<MessageDto>(
          `${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/messages/${contactId}/send/quoted`,
          {
            method: "POST",
            body: JSON.stringify(body),
          },
        )

        if (response.hasError) throw new Error(response.message ?? "Erro ao enviar mensagem")

        let newMessage: MessageDto = {
          id: Date.now().toString(),
          content: text,
          sentAt: new Date(),
          status: MessageStatusEnum.Created,
          isFromMe: true,
          contactId: contactId,
          quotedMessage: message,
          quotedMessageId: message.id,
          contact: {
            lastMessage: text,
            lastMessageAt: Date.now().toString(),
          } as ContactDto,
          messageReactions: [],
        }

        if (response.data) newMessage = response.data

        setMessages((prev) => ({
          ...prev,
          [contactId]: [...(prev[contactId] || []), newMessage],
        }))
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Enviar reação
  const sendReactionMessage = useCallback(
    async (contactId: string, message: MessageDto, reaction: string) => {
      if (!contactId || !message.id || !reaction || !instanceId) return
      setError(null)

      try {
        const body = { reaction: reaction, key: { id: message.id } }

        const response = await secureApiCall<MessageDto>(
          `${API_CONFIG.ENDPOINTS.CHAT}/${instanceId}/messages/${contactId}/send/reaction`,
          {
            method: "POST",
            body: JSON.stringify(body),
          },
        )

        if (response.hasError) throw new Error(response.message ?? "Erro ao enviar a reação da mensagem")

        setMessages((prev) => {
          const messages = prev[contactId] || []
          const updated = messages.map((m) => {
            if (m.id !== message.id) return m

            return {
              ...m,
              reactions: [
                ...(m.messageReactions || []),
                {
                  reaction: reaction,
                  fromMe: true,
                  createdAt: new Date(),
                },
              ],
            }
          })

          return {
            ...prev,
            [contactId]: updated,
          }
        })
      } catch (err) {
        setError(err instanceof Error ? err.message : "Erro desconhecido")
      }
    },
    [instanceId],
  )

  // 🔹 Atualizar mensagens periodicamente (simulação de webhook)
  useEffect(() => {
    if (!selectedContactId || !instanceId) return
    const interval = setInterval(() => {
      fetchMessages(selectedContactId)
    }, 5000)
    return () => clearInterval(interval)
  }, [selectedContactId, fetchMessages, instanceId])

  // 🔹 Buscar contatos e conversas ao iniciar
  useEffect(() => {
    if (instanceId) {
      fetchContacts()
    }
  }, [fetchContacts, instanceId])

  return {
    contacts,
    messages,
    selectedContactId,
    setSelectedContactId,
    fetchContacts,
    saveContact,
    updateContact,
    fetchMessages,
    sendMessage,
    sendAttachment,
    deleteMessage,
    forwardMessage,
    sendQuotedMessage,
    sendReactionMessage,
    loading,
    error,
    setError,
    clearError: () => setError(null),
  }
}

"use client"

import type React from "react"

import { useEffect } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { MessageBubble } from "@/components/message-bubble"
import { Search, Send, MoreVertical, Loader2 } from "lucide-react"
import { cn } from "@/lib/utils"
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import { useEvolutionChat } from "@/hooks/use-evolution-chat.hook"
import { useState } from "react"
import { AuthGuard } from "@/components/auth/auth.guard"

export default function MessagesPage() {
  const { contacts, messages, selectedContactId, setSelectedContactId, fetchMessages, sendMessage, loading, error } =
    useEvolutionChat()

  const [messageText, setMessageText] = useState("")
  const [searchQuery, setSearchQuery] = useState("")

  const selectedContact = contacts.find((c) => c.id === selectedContactId)
  const currentMessages = selectedContactId ? messages[selectedContactId] || [] : []

  useEffect(() => {
    if (selectedContactId) {
      fetchMessages(selectedContactId)
    }
  }, [selectedContactId, fetchMessages])

  const filteredContacts = contacts.filter((contact) =>
    contact.displayName?.toLowerCase().includes(searchQuery.toLowerCase()),
  )

  const handleSendMessage = async (e: React.FormEvent) => {
    e.preventDefault()
    if (messageText.trim() && selectedContactId) {
      await sendMessage(selectedContactId, messageText)
      setMessageText("")
    }
  }

  const formatTime = (date?: Date | string) => {
    if (!date) return ""
    const d = new Date(date)
    const now = new Date()
    const diffDays = Math.floor((now.getTime() - d.getTime()) / (1000 * 60 * 60 * 24))

    if (diffDays === 0) {
      return d.toLocaleTimeString("pt-BR", { hour: "2-digit", minute: "2-digit" })
    }
    if (diffDays === 1) return "Ontem"
    if (diffDays < 7) return `${diffDays} dias`
    return d.toLocaleDateString("pt-BR")
  }

  return (
    <AuthGuard requiredRoles={["Admin"]}>
      <div className="min-h-screen bg-background p-4 md:p-8">
        <div className="flex flex-1 overflow-hidden">
          {/* Contacts List */}
          <div className="w-80 border-r flex flex-col bg-card">
            <div className="p-4 border-b">
              <h2 className="text-lg font-semibold mb-3">Mensagens</h2>
              <div className="relative">
                <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                <Input
                  type="search"
                  placeholder="Buscar contato..."
                  className="pl-10"
                  value={searchQuery}
                  onChange={(e) => setSearchQuery(e.target.value)}
                />
              </div>
            </div>

            <div className="flex-1 overflow-y-auto">
              {loading && contacts.length === 0 ? (
                <div className="flex items-center justify-center py-8">
                  <Loader2 className="h-6 w-6 animate-spin text-muted-foreground" />
                </div>
              ) : filteredContacts.length === 0 ? (
                <div className="flex items-center justify-center py-8">
                  <p className="text-muted-foreground text-sm">
                    {searchQuery ? "Nenhum contato encontrado" : "Nenhum contato"}
                  </p>
                </div>
              ) : (
                filteredContacts.map((contact) => (
                  <button
                    key={contact.id}
                    onClick={() => setSelectedContactId(`${contact.id}`)}
                    className={cn(
                      "w-full p-4 flex items-start gap-3 border-b hover:bg-muted/50 transition-colors text-left",
                      selectedContactId === contact.id && "bg-muted",
                    )}
                  >
                    <Avatar className="h-12 w-12">
                      <AvatarImage src={contact.profilePictureUrl || "/placeholder.svg"} alt={contact.displayName} />
                      <AvatarFallback>
                        {`${contact.displayName}`
                          .split(" ")
                          .map((n) => n[0])
                          .join("")}
                      </AvatarFallback>
                    </Avatar>

                    <div className="flex-1 min-w-0">
                      <div className="flex items-center justify-between mb-1">
                        <p className="font-medium truncate">{contact.displayName}</p>
                        <span className="text-xs text-muted-foreground">{formatTime(contact.lastMessageAt)}</span>
                      </div>
                      <div className="flex items-center justify-between">
                        <p className="text-sm text-muted-foreground truncate">
                          {contact.lastMessage || "Sem mensagens"}
                        </p>
                        {contact.unread && contact.unread > 0 && (
                          <Badge className="ml-2 h-5 w-5 rounded-full p-0 flex items-center justify-center bg-primary text-primary-foreground text-xs">
                            {contact.unread}
                          </Badge>
                        )}
                      </div>
                    </div>
                  </button>
                ))
              )}
            </div>
          </div>

          {/* Chat Area */}
          <div className="flex-1 flex flex-col">
            {selectedContact ? (
              <>
                {/* Chat Header */}
                <div className="h-16 border-b flex items-center justify-between px-6 bg-card">
                  <div className="flex items-center gap-3">
                    <Avatar>
                      <AvatarImage
                        src={selectedContact.profilePictureUrl || "/placeholder.svg"}
                        alt={selectedContact.displayName}
                      />
                      <AvatarFallback>
                        {`${selectedContact.displayName}`
                          .split(" ")
                          .map((n) => n[0])
                          .join("")}
                      </AvatarFallback>
                    </Avatar>
                    <div>
                      <p className="font-medium">{selectedContact.displayName}</p>
                      <p className="text-xs text-muted-foreground">{selectedContact.number}</p>
                    </div>
                  </div>

                  <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                      <Button variant="ghost" size="icon">
                        <MoreVertical className="h-5 w-5" />
                      </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                      <DropdownMenuItem>Ver perfil do aluno</DropdownMenuItem>
                      <DropdownMenuItem>Ver treinos</DropdownMenuItem>
                      <DropdownMenuItem>Arquivar conversa</DropdownMenuItem>
                    </DropdownMenuContent>
                  </DropdownMenu>
                </div>

                {/* Messages */}
                <div className="flex-1 overflow-y-auto p-6 bg-muted/20">
                  {currentMessages.length === 0 ? (
                    <div className="flex items-center justify-center h-full">
                      <p className="text-muted-foreground">Nenhuma mensagem ainda</p>
                    </div>
                  ) : (
                    currentMessages.map((msg) => (
                      <MessageBubble
                        key={msg.id}
                        message={msg.content}
                        timestamp={formatTime(msg.sentAt)}
                        type={msg.isFromMe ? "sent" : "received"}
                        isAutomatic={false}
                      />
                    ))
                  )}
                </div>

                {/* Message Input */}
                <div className="border-t bg-card p-4">
                  <form onSubmit={handleSendMessage} className="flex gap-2">
                    <Input
                      value={messageText}
                      onChange={(e) => setMessageText(e.target.value)}
                      placeholder="Digite sua mensagem..."
                      className="flex-1"
                    />
                    <Button type="submit" size="icon" disabled={!messageText.trim()}>
                      <Send className="h-4 w-4" />
                    </Button>
                  </form>
                  <p className="text-xs text-muted-foreground mt-2">Integrado com WhatsApp via Evolution API</p>
                </div>
              </>
            ) : (
              <div className="flex-1 flex items-center justify-center bg-muted/20">
                <p className="text-muted-foreground">Selecione uma conversa para começar</p>
              </div>
            )}
          </div>
        </div>
      </div>
    </AuthGuard>
  )
}

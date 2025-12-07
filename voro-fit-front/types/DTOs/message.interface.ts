import type { ContactDto } from "./contact.interface"

export enum MessageStatusEnum {
  Created = 1,
  Sent = 2,
  Delivered = 3,
  Read = 4,
  Failed = 5,
}

export interface MessageReactionDto {
  reaction: string
  fromMe: boolean
  createdAt: Date
}

export interface MessageDto {
  id: string
  content: string
  base64?: string
  sentAt: Date
  status: MessageStatusEnum
  isFromMe: boolean
  contactId: string
  contact?: ContactDto
  quotedMessage?: MessageDto
  quotedMessageId?: string
  messageReactions: MessageReactionDto[]
}

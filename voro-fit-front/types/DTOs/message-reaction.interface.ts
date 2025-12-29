import { ContactDto } from "./contact.interface";
import { MessageDto } from "./message.interface";

export interface MessageReactionDto {
  id: string;
  reaction: string;
  contactId?: string;
  contact?: ContactDto;
  createdAt: Date;
  messageId?: string;
  message?: MessageDto;
}

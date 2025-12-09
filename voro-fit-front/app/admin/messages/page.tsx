import { AuthGuard } from "@/components/auth/auth.guard";
import Messages from "@/components/layout/admin/messages/messages";


export default function MessagesPage() {
  return (
    <AuthGuard requiredRoles={["Trainer"]}>
      <Messages />
    </AuthGuard>
  );
}

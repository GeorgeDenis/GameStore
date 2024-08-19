export interface INotification {
    notificationId: string,
    userId: string,
    subjectName: string,
    notificationType: string,
    dateSent?: Date,
    readStatus: boolean
}
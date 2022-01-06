using NotificationSamples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples.Android;
public class NotificationTest : MonoBehaviour
{
    public const string ChannelId = "game_channel0";

    [SerializeField]
    protected GameNotificationsManager manager;

    private void Start()
    {
        // Set up channels (mostly for Android)
        // You need to have at least one of these
        var c1 = new GameNotificationChannel(ChannelId, "Default Game Channel", "Generic notifications");

        manager.Initialize(c1);
    }

    public void OnClickBtnNoti()
    {
        manager.CancelNotificationByChannel(ChannelId);
        DateTime deliveryTime = DateTime.Now.ToLocalTime().AddSeconds(5);

        SendNotification("Test Notification", "Remember to make more cookies!", deliveryTime, channelId: ChannelId);
    }
    public void SendNotification(string title, string body, DateTime deliveryTime, int? badgeNumber = null, bool reschedule = false, string channelId = null, string smallIcon = null, string largeIcon = null)
    {
        IGameNotification notification = manager.CreateNotification();

        if (notification == null)
        {
            return;
        }
        notification.Title = "<color=yellow>RICH Test 1605 </color>";
        notification.Body = null;
        //notification.Body = body;
        notification.Group = !string.IsNullOrEmpty(channelId) ? channelId : ChannelId;
        notification.DeliveryTime = deliveryTime;
        notification.SmallIcon = smallIcon;
        notification.LargeIcon = largeIcon;
        if (badgeNumber != null)
        {
            notification.BadgeNumber = badgeNumber;
        }
        manager.ScheduleNotification(notification);
    }

    //public void SendNotification(string title, string body, DateTime deliveryTime, int? badgeNumber = null,
    //       bool reschedule = false, string channelId = null,
    //       string smallIcon = null, string largeIcon = null)
    //{
    //    IGameNotification notification = manager.CreateNotification();

    //    if (notification == null)
    //    {
    //        return;
    //    }

    //    notification.Title = title;
    //    notification.Body = body;
    //    notification.Group = !string.IsNullOrEmpty(channelId) ? channelId : ChannelId;
    //    notification.DeliveryTime = deliveryTime;
    //    notification.SmallIcon = smallIcon;
    //    notification.LargeIcon = largeIcon;
    //    if (badgeNumber != null)
    //    {
    //        notification.BadgeNumber = badgeNumber;
    //    }

    //    PendingNotification notificationToDisplay = manager.ScheduleNotification(notification);
    //    notificationToDisplay.Reschedule = reschedule;
    //    updatePendingNotifications = true;

    //    QueueEvent($"Queued event with ID \"{notification.Id}\" at time {deliveryTime:HH:mm}");
    //}

}
